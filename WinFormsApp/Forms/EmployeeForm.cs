using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EmployeeForm : Form
    {
        private EmployeeService _employeeService;
        private DepartmentService _departmentService;
        private BindingSource _bindingSource = new BindingSource();

        public EmployeeForm(EmployeeService employeeService, DepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;

            InitializeComponent();

            // Добавим отладочную информацию
            Console.WriteLine("EmployeeForm: Конструктор вызван");

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Console.WriteLine("EmployeeForm: Начало загрузки данных...");

                var employees = _employeeService.GetAll().ToList();

                Console.WriteLine($"EmployeeForm: Получено {employees.Count} сотрудников");

                if (employees.Count > 0)
                {
                    Console.WriteLine("Первый сотрудник: " + employees[0].FullName);
                }

                _bindingSource.DataSource = employees;
                dataGridView1.DataSource = _bindingSource;

                // Настройте колонки DataGridView
                ConfigureDataGridView();

                lblStatus.Text = $"Записей: {employees.Count}";

                Console.WriteLine("EmployeeForm: Данные загружены успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EmployeeForm: Ошибка: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                // Настройте колонки
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Добавьте колонки вручную
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    HeaderText = "ID",
                    DataPropertyName = "Id",
                    Width = 50,
                    ReadOnly = true
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "FullName",
                    HeaderText = "ФИО",
                    DataPropertyName = "FullName",
                    Width = 200
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Position",
                    HeaderText = "Должность",
                    DataPropertyName = "Position",
                    Width = 150
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DepartmentName",
                    HeaderText = "Подразделение",
                    DataPropertyName = "DepartmentName",
                    Width = 150
                });

                Console.WriteLine("DataGridView настроен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка настройки DataGridView: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка Добавить нажата");

            using (var editForm = new EmployeeEditForm(_departmentService))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _employeeService.Create(new EmployeeCreateDTO
                        {
                            FullName = editForm.FullName,
                            Position = editForm.Position,
                            DepartmentId = editForm.DepartmentId
                        });

                        MessageBox.Show("Сотрудник успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка Изменить нажата");

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var employee = (EmployeeDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            using (var editForm = new EmployeeEditForm(_departmentService,
                employee.FullName, employee.Position, employee.DepartmentId))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        employee.FullName = editForm.FullName;
                        employee.Position = editForm.Position;
                        employee.DepartmentId = editForm.DepartmentId;

                        _employeeService.Update(employee);

                        MessageBox.Show("Данные сотрудника обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка Удалить нажата");

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var employee = (EmployeeDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Удалить сотрудника '{employee.FullName}'?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _employeeService.Delete(employee.Id);

                    MessageBox.Show("Сотрудник удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка Обновить нажата");
            LoadData();
        }

        // Добавьте обработчик загрузки формы для отладки
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("EmployeeForm загружен");
        }
    }
}