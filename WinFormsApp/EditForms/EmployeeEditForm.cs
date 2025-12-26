using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EmployeeEditForm : Form
    {
        // Свойства для доступа к данным
        public string FullName { get; private set; }
        public string Position { get; private set; }
        public int DepartmentId { get; private set; }

        private DepartmentService _departmentService;

        // Конструктор для добавления нового сотрудника
        public EmployeeEditForm(DepartmentService departmentService)
        {
            _departmentService = departmentService;
            InitializeComponent();
            LoadDepartments();
        }

        // Конструктор для редактирования существующего сотрудника
        public EmployeeEditForm(DepartmentService departmentService,
                               string fullName, string position, int departmentId)
            : this(departmentService)
        {
            txtFullName.Text = fullName;
            txtPosition.Text = position;

            // Устанавливаем выбранное подразделение
            if (cmbDepartment.Items.Count > 0)
            {
                // Ищем подразделение по ID
                for (int i = 0; i < cmbDepartment.Items.Count; i++)
                {
                    if (cmbDepartment.Items[i] is DepartmentDTO dept && dept.Id == departmentId)
                    {
                        cmbDepartment.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        // Загрузка списка подразделений в ComboBox
        private void LoadDepartments()
        {
            try
            {
                var departments = _departmentService.GetAll().ToList();
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "Id";

                // Выбираем первый элемент, если есть
                if (departments.Count > 0)
                {
                    cmbDepartment.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подразделений: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Сохранить"
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            // Сохраняем значения из формы
            FullName = txtFullName.Text.Trim();
            Position = txtPosition.Text.Trim();

            // Получаем ID выбранного подразделения
            if (cmbDepartment.SelectedItem is DepartmentDTO selectedDept)
            {
                DepartmentId = selectedDept.Id;
            }
            else
            {
                MessageBox.Show("Не удалось получить подразделение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // Валидация формы
        private bool ValidateForm()
        {
            // Проверка ФИО
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("ФИО сотрудника не может быть пустым", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Проверка должности
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Должность не может быть пустой", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPosition.Focus();
                return false;
            }

            // Проверка подразделения
            if (cmbDepartment.SelectedItem == null)
            {
                MessageBox.Show("Выберите подразделение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartment.Focus();
                return false;
            }

            return true;
        }

        // Кнопка "Отмена"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Нажатие Enter в поле ФИО
        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPosition.Focus();
            }
        }

        // Нажатие Enter в поле должности
        private void txtPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, e);
            }
        }
    }
}