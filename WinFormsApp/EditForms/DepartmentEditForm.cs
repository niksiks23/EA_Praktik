using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class DepartmentEditForm : Form
    {
        public string DepartmentName { get; private set; }
        public string DepartmentHead { get; private set; }

        public DepartmentEditForm(string name = "", string head = "")
        {
            InitializeComponent();
            txtName.Text = name;
            txtHead.Text = head;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название", "Ошибка");
                return;
            }

            DepartmentName = txtName.Text.Trim();
            DepartmentHead = txtHead.Text.Trim();
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