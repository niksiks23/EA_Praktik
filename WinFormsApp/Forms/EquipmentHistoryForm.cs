using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class EquipmentHistoryForm : Form
    {
        private EquipmentHistoryService _historyService;
        private EquipmentService _equipmentService;
        private BindingSource _bindingSource = new BindingSource();

        public EquipmentHistoryForm(EquipmentHistoryService historyService, EquipmentService equipmentService)
        {
            _historyService = historyService;
            _equipmentService = equipmentService;

            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var history = _historyService.GetAll().ToList();
                _bindingSource.DataSource = history;
                dataGridView1.DataSource = _bindingSource;
                lblStatus.Text = $"Записей: {history.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnViewByEquipment_Click(object sender, EventArgs e)
        {
            using (var selectForm = new EquipmentSelectForm(_equipmentService))
            {
                if (selectForm.ShowDialog() == DialogResult.OK && selectForm.SelectedEquipmentId.HasValue)
                {
                    try
                    {
                        var history = _historyService.GetByEquipmentId(selectForm.SelectedEquipmentId.Value).ToList();
                        _bindingSource.DataSource = history;
                        dataGridView1.DataSource = _bindingSource;
                        lblStatus.Text = $"Записей для оборудования: {history.Count}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления", "Информация");
                return;
            }

            var history = (EquipmentHistoryDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Удалить запись истории от {history.ChangeDate:dd.MM.yyyy}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _historyService.Delete(history.Id);
                    LoadData();
                    MessageBox.Show("Запись удалена", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }
    }
}