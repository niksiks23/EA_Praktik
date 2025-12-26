using BLL.DTOs;
using BLL.Services;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EquipmentHistoryEditForm : Form
    {
        // Свойства для доступа к данным
        public int EquipmentId { get; private set; }
        public DateTime ChangeDate { get; private set; }
        public int? OldEmployeeId { get; private set; }
        public int? NewEmployeeId { get; private set; }
        public string Reason { get; private set; }
        public string Notes { get; private set; }

        private EquipmentService _equipmentService;
        private List<EquipmentItem> _equipmentList;
        private List<EmployeeItem> _employees;

        // Конструктор для добавления новой записи
        public EquipmentHistoryEditForm(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadData();
            SetDefaultValues();
        }

        // Конструктор для редактирования существующей записи
        public EquipmentHistoryEditForm(EquipmentService equipmentService, EquipmentHistoryDTO history)
            : this(equipmentService)
        {
            // Заполняем поля данными
            foreach (var item in cmbEquipment.Items)
            {
                if (item is EquipmentItem eq && eq.Id == history.EquipmentId)
                {
                    cmbEquipment.SelectedItem = item;
                    break;
                }
            }

            dtpChangeDate.Value = history.ChangeDate;

            // Старый сотрудник
            if (history.OldEmployeeId.HasValue)
            {
                foreach (var item in cmbOldEmployee.Items)
                {
                    if (item is EmployeeItem emp && emp.Id == history.OldEmployeeId.Value)
                    {
                        cmbOldEmployee.SelectedItem = item;
                        break;
                    }
                }
            }

            // Новый сотрудник
            if (history.NewEmployeeId.HasValue)
            {
                foreach (var item in cmbNewEmployee.Items)
                {
                    if (item is EmployeeItem emp && emp.Id == history.NewEmployeeId.Value)
                    {
                        cmbNewEmployee.SelectedItem = item;
                        break;
                    }
                }
            }

            txtReason.Text = history.Reason;
            txtNotes.Text = history.Notes;
        }

        private void LoadData()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Загружаем оборудование с сотрудниками
                    _equipmentList = context.Equipments
                        .Include(e => e.Employee)
                        .OrderBy(e => e.InventoryNumber)
                        .Select(e => new EquipmentItem
                        {
                            Id = e.Id,
                            InventoryNumber = e.InventoryNumber,
                            Name = e.Name,
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.Employee != null ? e.Employee.FullName : null
                        })
                        .ToList();

                    cmbEquipment.DataSource = _equipmentList;
                    cmbEquipment.DisplayMember = "DisplayInfo";
                    cmbEquipment.ValueMember = "Id";

                    // Загружаем сотрудников
                    _employees = context.Employees
                        .OrderBy(e => e.FullName)
                        .Select(e => new EmployeeItem
                        {
                            Id = e.Id,
                            FullName = e.FullName
                        })
                        .ToList();

                    // Старый сотрудник
                    var oldEmployees = new List<object> { new { Id = 0, FullName = "Не указан" } };
                    oldEmployees.AddRange(_employees.Cast<object>());
                    cmbOldEmployee.DataSource = oldEmployees;
                    cmbOldEmployee.DisplayMember = "FullName";
                    cmbOldEmployee.ValueMember = "Id";

                    // Новый сотрудник
                    var newEmployees = new List<object> { new { Id = 0, FullName = "Не указан" } };
                    newEmployees.AddRange(_employees.Cast<object>());
                    cmbNewEmployee.DataSource = newEmployees;
                    cmbNewEmployee.DisplayMember = "FullName";
                    cmbNewEmployee.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка");
            }
        }

        private void SetDefaultValues()
        {
            dtpChangeDate.Value = DateTime.Now;
            cmbOldEmployee.SelectedIndex = 0;
            cmbNewEmployee.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            // Сохраняем значения из формы
            if (cmbEquipment.SelectedItem is EquipmentItem selectedEquipment)
            {
                EquipmentId = selectedEquipment.Id;
            }
            else
            {
                MessageBox.Show("Выберите оборудование", "Ошибка");
                return;
            }

            ChangeDate = dtpChangeDate.Value;

            if (cmbOldEmployee.SelectedIndex == 0) // "Не указан"
            {
                OldEmployeeId = null;
            }
            else if (cmbOldEmployee.SelectedItem is EmployeeItem oldEmp)
            {
                OldEmployeeId = oldEmp.Id;
            }

            if (cmbNewEmployee.SelectedIndex == 0) // "Не указан"
            {
                NewEmployeeId = null;
            }
            else if (cmbNewEmployee.SelectedItem is EmployeeItem newEmp)
            {
                NewEmployeeId = newEmp.Id;
            }

            Reason = txtReason.Text.Trim();
            Notes = txtNotes.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            // Проверка оборудования
            if (cmbEquipment.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование", "Ошибка");
                cmbEquipment.Focus();
                return false;
            }

            // Проверка причины
            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Укажите причину изменения", "Ошибка");
                txtReason.Focus();
                return false;
            }

            // Проверка, что старый и новый сотрудник не одинаковые
            if (cmbOldEmployee.SelectedIndex > 0 && cmbNewEmployee.SelectedIndex > 0)
            {
                if (cmbOldEmployee.SelectedItem is EmployeeItem oldEmp &&
                    cmbNewEmployee.SelectedItem is EmployeeItem newEmp &&
                    oldEmp.Id == newEmp.Id)
                {
                    MessageBox.Show("Старый и новый сотрудник не могут быть одинаковыми", "Ошибка");
                    return false;
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedItem is EquipmentItem equipment && equipment.EmployeeId.HasValue)
            {
                // Автоматически заполняем старого сотрудника текущим ответственным
                foreach (var item in cmbOldEmployee.Items)
                {
                    if (item is EmployeeItem emp && emp.Id == equipment.EmployeeId.Value)
                    {
                        cmbOldEmployee.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    // Класс для отображения информации об оборудовании
    public class EquipmentItem
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string DisplayInfo
        {
            get
            {
                var empInfo = !string.IsNullOrEmpty(EmployeeName) ? $" ({EmployeeName})" : "";
                return $"{InventoryNumber} - {Name}{empInfo}";
            }
        }
    }

    // Класс для отображения информации о сотрудниках
    public class EmployeeItem
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}