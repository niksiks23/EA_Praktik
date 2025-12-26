using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EquipmentForm : Form, IEquipmentObserver
    {
        private EquipmentService _equipmentService;
        private EmployeeService _employeeService;
        private BindingSource _bindingSource = new BindingSource();

        public EquipmentForm(EquipmentService equipmentService, EmployeeService employeeService)
        {
            _equipmentService = equipmentService;
            _employeeService = employeeService;

            InitializeComponent();

            // Подписываемся на уведомления
            if (_equipmentService is IObservableEquipmentService observable)
            {
                observable.Subscribe(this);
                Console.WriteLine("EquipmentForm: Подписан на уведомления");
            }

            LoadData();
        }

        // ========== РЕАЛИЗАЦИЯ IEquipmentObserver ==========
        public void OnEquipmentChanged()
        {
            Console.WriteLine("EquipmentForm: Получено уведомление об изменении оборудования");

            // Обновляем данные в UI потоке
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadData()));
            }
            else
            {
                LoadData();
            }
        }

        public void OnEmployeeChanged()
        {
            Console.WriteLine("EquipmentForm: Получено уведомление об изменении сотрудников");

            // Обновляем поиск, если он активен
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => txtSearch_TextChanged(null, EventArgs.Empty)));
                }
                else
                {
                    txtSearch_TextChanged(null, EventArgs.Empty);
                }
            }
        }

        public void OnEquipmentTypeChanged()
        {
            Console.WriteLine("EquipmentForm: Получено уведомление об изменении типов оборудования");
            // Можно добавить обновление ComboBox'ов при необходимости
        }

        // Отписываемся при закрытии формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_equipmentService is IObservableEquipmentService observable)
            {
                observable.Unsubscribe(this);
                Console.WriteLine("EquipmentForm: Отписан от уведомлений");
            }
        }

        // ========== ОСНОВНЫЕ МЕТОДЫ ==========
        private void LoadData()
        {
            try
            {
                Console.WriteLine("EquipmentForm: Загрузка данных...");
                var equipment = _equipmentService.GetAll().ToList();
                _bindingSource.DataSource = equipment;
                dataGridView1.DataSource = _bindingSource;
                lblStatus.Text = $"Записей: {equipment.Count}";
                Console.WriteLine($"EquipmentForm: Загружено {equipment.Count} записей");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EquipmentEditForm(_equipmentService, _employeeService))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _equipmentService.Create(new EquipmentCreateDTO
                        {
                            InventoryNumber = editForm.InventoryNumber,
                            Name = editForm.Name,
                            EquipmentTypeId = editForm.EquipmentTypeId,
                            SerialNumber = editForm.SerialNumber,
                            EmployeeId = editForm.EmployeeId,
                            RegistrationDate = editForm.RegistrationDate,
                            PurchaseDate = editForm.PurchaseDate,
                            PurchasePrice = editForm.PurchasePrice,
                            Status = editForm.Status,
                            Location = editForm.Location,
                            Specifications = editForm.Specifications
                        });

                        MessageBox.Show("Оборудование успешно добавлено", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Данные обновятся автоматически через наблюдатель
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите оборудование для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var equipment = (EquipmentDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            using (var editForm = new EquipmentEditForm(_equipmentService, _employeeService, equipment))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        equipment.InventoryNumber = editForm.InventoryNumber;
                        equipment.Name = editForm.Name;
                        equipment.EquipmentTypeId = editForm.EquipmentTypeId;
                        equipment.SerialNumber = editForm.SerialNumber;
                        equipment.EmployeeId = editForm.EmployeeId;
                        equipment.RegistrationDate = editForm.RegistrationDate;
                        equipment.PurchaseDate = editForm.PurchaseDate;
                        equipment.PurchasePrice = editForm.PurchasePrice;
                        equipment.Status = editForm.Status;
                        equipment.Location = editForm.Location;
                        equipment.Specifications = editForm.Specifications;

                        _equipmentService.Update(equipment);

                        MessageBox.Show("Данные оборудования обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Данные обновятся автоматически через наблюдатель
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите оборудование для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var equipment = (EquipmentDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Удалить оборудование '{equipment.Name}' (инв. № {equipment.InventoryNumber})?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _equipmentService.Delete(equipment.Id);

                    MessageBox.Show("Оборудование удалено", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Данные обновятся автоматически через наблюдатель
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
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadData();
                }
                else
                {
                    var results = _equipmentService.Search(searchText).ToList();
                    _bindingSource.DataSource = results;
                    dataGridView1.DataSource = _bindingSource;
                    lblStatus.Text = $"Найдено: {results.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestObserver_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Тест паттерна 'Наблюдатель':\n\n" +
                "1. Откройте форму 'Сотрудники' (Данные → Сотрудники)\n" +
                "2. Добавьте нового сотрудника или измените существующего\n" +
                "3. Вернитесь в форму 'Оборудование'\n" +
                "4. Если был активен поиск по сотрудникам, результаты обновятся автоматически\n\n" +
                "5. Также работает при:\n" +
                "   - Добавлении/изменении оборудования в другой форме\n" +
                "   - Удалении оборудования\n" +
                "   - Изменении типа оборудования",
                "Тестирование паттерна Наблюдатель",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                btnSimpleReport_Click();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка формирования отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimpleReport_Click()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Получаем отчет
                var report = _equipmentService.GetSimpleEquipmentReport();

                // Создаем форму для отчета
                var form = new Form
                {
                    Text = "Отчет по оборудованию",
                    Size = new Size(700, 500),
                    StartPosition = FormStartPosition.CenterParent
                };

                var textBox = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    Text = report,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Consolas", 9),
                    BackColor = Color.White
                };

                var btnClose = new Button
                {
                    Text = "Закрыть",
                    Dock = DockStyle.Bottom,
                    Height = 40
                };
                btnClose.Click += (s, args) => form.Close();

                var btnSave = new Button
                {
                    Text = "Сохранить в файл",
                    Dock = DockStyle.Bottom,
                    Height = 40
                };
                btnSave.Click += (s, args) =>
                {
                    SaveFileDialog saveDialog = new SaveFileDialog
                    {
                        Filter = "Текстовые файлы (*.txt)|*.txt",
                        FileName = $"Отчет_{DateTime.Now:yyyyMMdd}.txt"
                    };

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(saveDialog.FileName, report);
                        MessageBox.Show("Отчет сохранен!", "Успех");
                    }
                };

                form.Controls.Add(textBox);
                form.Controls.Add(btnSave);
                form.Controls.Add(btnClose);

                Cursor = Cursors.Default;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}