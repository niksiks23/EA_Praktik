using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EquipmentSelectForm : Form
    {
        public int? SelectedEquipmentId { get; private set; }

        private EquipmentService _equipmentService;
        private BindingSource _bindingSource = new BindingSource();

        public EquipmentSelectForm(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var equipment = _equipmentService.GetAll().ToList();
                _bindingSource.DataSource = equipment;
                dataGridView1.DataSource = _bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите оборудование", "Информация");
                return;
            }

            var equipment = (EquipmentDTO)dataGridView1.SelectedRows[0].DataBoundItem;
            SelectedEquipmentId = equipment.Id;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}