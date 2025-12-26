using BLL.DTOs;
using BLL.Services;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsApp
{
    public partial class EquipmentEditForm : Form
    {
        // Свойства для доступа к данным
        public string InventoryNumber { get; private set; }
        public string Name { get; private set; }
        public int EquipmentTypeId { get; private set; }
        public string SerialNumber { get; private set; }
        public int? EmployeeId { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public DateTime? PurchaseDate { get; private set; }
        public decimal? PurchasePrice { get; private set; }
        public string Status { get; private set; }
        public string Location { get; private set; }
        public string Specifications { get; private set; }

        private EquipmentService _equipmentService;
        private EmployeeService _employeeService;
        private List<EquipmentType> _equipmentTypes;
        private List<EmployeeDTO> _employees;
        private List<string> _statuses;

        // Конструктор для добавления нового оборудования
        public EquipmentEditForm(EquipmentService equipmentService, EmployeeService employeeService)
        {
            _equipmentService = equipmentService;
            _employeeService = employeeService;

            InitializeComponent();
            LoadData();
            SetDefaultValues();
        }

        // Конструктор для редактирования существующего оборудования
        public EquipmentEditForm(EquipmentService equipmentService, EmployeeService employeeService, EquipmentDTO equipment)
            : this(equipmentService, employeeService)
        {
            // Заполняем поля данными оборудования
            txtInventoryNumber.Text = equipment.InventoryNumber;
            txtName.Text = equipment.Name;

            // Тип оборудования
            foreach (var item in cmbEquipmentType.Items)
            {
                if (item is EquipmentType et && et.Id == equipment.EquipmentTypeId)
                {
                    cmbEquipmentType.SelectedItem = item;
                    break;
                }
            }

            txtSerialNumber.Text = equipment.SerialNumber;

            // Сотрудник
            if (equipment.EmployeeId.HasValue)
            {
                foreach (var item in cmbEmployee.Items)
                {
                    if (item is EmployeeDTO emp && emp.Id == equipment.EmployeeId.Value)
                    {
                        cmbEmployee.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                cmbEmployee.SelectedIndex = 0; // "Не закреплено"
            }

            dtpRegistrationDate.Value = equipment.RegistrationDate;

            if (equipment.PurchaseDate.HasValue)
            {
                dtpPurchaseDate.Value = equipment.PurchaseDate.Value;
                chkPurchaseDate.Checked = true;
            }
            else
            {
                chkPurchaseDate.Checked = false;
                dtpPurchaseDate.Enabled = false;
            }

            if (equipment.PurchasePrice.HasValue)
            {
                txtPurchasePrice.Text = equipment.PurchasePrice.Value.ToString("0.00");
                chkPurchasePrice.Checked = true;
            }
            else
            {
                chkPurchasePrice.Checked = false;
                txtPurchasePrice.Enabled = false;
            }

            // Статус
            foreach (var item in cmbStatus.Items)
            {
                if (item.ToString() == equipment.Status)
                {
                    cmbStatus.SelectedItem = item;
                    break;
                }
            }

            txtLocation.Text = equipment.Location;
            txtSpecifications.Text = equipment.Specifications;
        }

        private void LoadData()
        {
            try
            {
                // Загружаем типы оборудования
                using (var context = new AppDbContext())
                {
                    _equipmentTypes = context.EquipmentTypes.ToList();
                    cmbEquipmentType.DataSource = _equipmentTypes;
                    cmbEquipmentType.DisplayMember = "Name";
                    cmbEquipmentType.ValueMember = "Id";
                }

                // Загружаем сотрудников
                _employees = _employeeService.GetAll();
                // Добавляем опцию "Не закреплено"
                var employeesList = new List<object> { new { Id = 0, FullName = "Не закреплено" } };
                employeesList.AddRange(_employees.Cast<object>());
                cmbEmployee.DataSource = employeesList;
                cmbEmployee.DisplayMember = "FullName";
                cmbEmployee.ValueMember = "Id";

                // Загружаем статусы
                _statuses = _equipmentService.GetStatuses();
                cmbStatus.DataSource = _statuses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            dtpRegistrationDate.Value = DateTime.Today;
            cmbStatus.SelectedIndex = 0; // "В работе"
            cmbEmployee.SelectedIndex = 0; // "Не закреплено"
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            // Сохраняем значения из формы
            InventoryNumber = txtInventoryNumber.Text.Trim();
            Name = txtName.Text.Trim();
            EquipmentTypeId = (cmbEquipmentType.SelectedItem as EquipmentType)?.Id ?? 0;
            SerialNumber = txtSerialNumber.Text.Trim();

            if (cmbEmployee.SelectedIndex == 0) // "Не закреплено"
            {
                EmployeeId = null;
            }
            else
            {
                EmployeeId = (cmbEmployee.SelectedItem as EmployeeDTO)?.Id;
            }

            RegistrationDate = dtpRegistrationDate.Value;

            PurchaseDate = chkPurchaseDate.Checked ? dtpPurchaseDate.Value : (DateTime?)null;

            if (chkPurchasePrice.Checked && decimal.TryParse(txtPurchasePrice.Text, out decimal price))
            {
                PurchasePrice = price;
            }
            else
            {
                PurchasePrice = null;
            }

            Status = cmbStatus.SelectedItem?.ToString() ?? "В работе";
            Location = txtLocation.Text.Trim();
            Specifications = txtSpecifications.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            // Проверка инвентарного номера
            if (string.IsNullOrWhiteSpace(txtInventoryNumber.Text))
            {
                MessageBox.Show("Инвентарный номер не может быть пустым", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInventoryNumber.Focus();
                return false;
            }

            // Проверка названия
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Название оборудования не может быть пустым", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // Проверка типа оборудования
            if (cmbEquipmentType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип оборудования", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEquipmentType.Focus();
                return false;
            }

            // Проверка цены (если указана)
            if (chkPurchasePrice.Checked)
            {
                if (!decimal.TryParse(txtPurchasePrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Укажите корректную цену покупки", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPurchasePrice.Focus();
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

        private void chkPurchaseDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpPurchaseDate.Enabled = chkPurchaseDate.Checked;
            if (!chkPurchaseDate.Checked)
            {
                dtpPurchaseDate.Value = DateTime.Today;
            }
        }

        private void chkPurchasePrice_CheckedChanged(object sender, EventArgs e)
        {
            txtPurchasePrice.Enabled = chkPurchasePrice.Checked;
            if (!chkPurchasePrice.Checked)
            {
                txtPurchasePrice.Text = "";
            }
        }
    }
}