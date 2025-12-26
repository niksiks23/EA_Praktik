using BLL.Services;
using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        private DepartmentService _departmentService;
        private EmployeeService _employeeService;
        private EquipmentService _equipmentService;
        private EquipmentHistoryService _historyService;
        private SoftwareLicenseService _licenseService;
        private InstalledSoftwareService _installedService;

        public MainForm(
            DepartmentService departmentService,
            EmployeeService employeeService,
            EquipmentService equipmentService,
            EquipmentHistoryService historyService,
            SoftwareLicenseService licenseService,
            InstalledSoftwareService installedService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _equipmentService = equipmentService;
            _historyService = historyService;
            _licenseService = licenseService;
            _installedService = installedService;

            InitializeComponent();

            // Добавляем новые пункты меню программно
            AddMenuItems();

            Console.WriteLine("MainForm: Создан");
        }

        private void AddMenuItems()
        {
            // Добавляем разделитель перед новыми пунктами
            var separator1 = new ToolStripSeparator();
            menuData.DropDownItems.Add(separator1);

            // Пункт "Лицензии ПО"
            var menuSoftwareLicenses = new ToolStripMenuItem("Лицензии ПО");
            menuSoftwareLicenses.Click += new EventHandler(menuSoftwareLicenses_Click);
            menuData.DropDownItems.Add(menuSoftwareLicenses);

            // Пункт "Установленное ПО"
            var menuInstalledSoftware = new ToolStripMenuItem("Установленное ПО");
            menuInstalledSoftware.Click += new EventHandler(menuInstalledSoftware_Click);
            menuData.DropDownItems.Add(menuInstalledSoftware);

            // Пункт "История перемещений"
            var menuHistory = new ToolStripMenuItem("История перемещений");
            menuHistory.Click += new EventHandler(menuHistory_Click);
            menuData.DropDownItems.Add(menuHistory);
        }

        // Существующие методы...
        private void OpenDepartmentForm()
        {
            Console.WriteLine("MainForm: Открытие формы подразделений");
            DepartmentForm form = new DepartmentForm(_departmentService);
            form.MdiParent = this;
            form.Show();
        }

        private void OpenEmployeeForm()
        {
            try
            {
                Console.WriteLine("MainForm: Открытие формы сотрудников");
                EmployeeForm form = new EmployeeForm(_employeeService, _departmentService);
                form.MdiParent = this;
                form.Show();
                Console.WriteLine("MainForm: Форма сотрудников открыта");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы сотрудников: {ex.Message}", "Ошибка");
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void OpenEquipmentForm()
        {
            try
            {
                Console.WriteLine("MainForm: Открытие формы оборудования");
                EquipmentForm form = new EquipmentForm(_equipmentService, _employeeService);
                form.MdiParent = this;
                form.Show();
                Console.WriteLine("MainForm: Форма оборудования открыта");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы оборудования: {ex.Message}", "Ошибка");
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");
            }
        }

        // Новые методы для ПО
        private void OpenSoftwareLicenseForm()
        {
            try
            {
                Console.WriteLine("MainForm: Открытие формы лицензий ПО");
                SoftwareLicenseForm form = new SoftwareLicenseForm(_licenseService);
                form.MdiParent = this;
                form.Show();
                Console.WriteLine("MainForm: Форма лицензий ПО открыта");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы лицензий ПО: {ex.Message}", "Ошибка");
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void OpenInstalledSoftwareForm()
        {
            try
            {
                Console.WriteLine("MainForm: Открытие формы установленного ПО");
                InstalledSoftwareForm form = new InstalledSoftwareForm(_installedService, _equipmentService, _licenseService);
                form.MdiParent = this;
                form.Show();
                Console.WriteLine("MainForm: Форма установленного ПО открыта");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы установленного ПО: {ex.Message}", "Ошибка");
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void OpenHistoryForm()
        {
            try
            {
                Console.WriteLine("MainForm: Открытие формы истории перемещений");
                EquipmentHistoryForm form = new EquipmentHistoryForm(_historyService, _equipmentService);
                form.MdiParent = this;
                form.Show();
                Console.WriteLine("MainForm: Форма истории открыта");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы истории: {ex.Message}", "Ошибка");
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");
            }
        }

        // Обработчики событий для новых пунктов меню
        private void menuSoftwareLicenses_Click(object sender, EventArgs e)
        {
            OpenSoftwareLicenseForm();
        }

        private void menuInstalledSoftware_Click(object sender, EventArgs e)
        {
            OpenInstalledSoftwareForm();
        }

        private void menuHistory_Click(object sender, EventArgs e)
        {
            OpenHistoryForm();
        }

        // Существующие обработчики...
        private void menuDepartments_Click(object sender, EventArgs e)
        {
            OpenDepartmentForm();
        }

        private void menuEmployees_Click(object sender, EventArgs e)
        {
            OpenEmployeeForm();
        }

        private void menuEquipment_Click(object sender, EventArgs e)
        {
            OpenEquipmentForm();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Учет оборудования v1.0\n\nСистема учета компьютерной техники и ПО",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "Учет оборудования и программного обеспечения";
            Console.WriteLine("MainForm загружен");
        }
    }
}