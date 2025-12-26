using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class DepartmentForm : Form
    {
        private DepartmentService _service;
        private BindingSource _bindingSource = new BindingSource();

        public DepartmentForm(DepartmentService service)
        {
            _service = service;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var departments = _service.GetAll().ToList();
                _bindingSource.DataSource = departments;
                dataGridView1.DataSource = _bindingSource;
                lblStatus.Text = $"Записей: {departments.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        // Добавить
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new DepartmentEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _service.Create(new DepartmentCreateDTO
                        {
                            Name = editForm.DepartmentName,
                            Head = editForm.DepartmentHead
                        });
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        // Изменить
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Информация");
                return;
            }

            var department = (DepartmentDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            using (var editForm = new DepartmentEditForm(department.Name, department.Head))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        department.Name = editForm.DepartmentName;
                        department.Head = editForm.DepartmentHead;
                        _service.Update(department);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        // Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Информация");
                return;
            }

            var department = (DepartmentDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Удалить '{department.Name}'?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _service.Delete(department.Id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
                }
            }
        }

        // Обновить
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}