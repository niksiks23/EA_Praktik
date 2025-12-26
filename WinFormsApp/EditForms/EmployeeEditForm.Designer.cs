namespace WinFormsApp
{
    partial class EmployeeEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtFullName;
        private TextBox txtPosition;
        private ComboBox cmbDepartment;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtFullName = new TextBox();
            txtPosition = new TextBox();
            cmbDepartment = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 0;
            label1.Text = "ФИО:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 60);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 1;
            label2.Text = "Должность:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 2;
            label3.Text = "Подразделение:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(130, 17);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 23);
            txtFullName.TabIndex = 3;
            txtFullName.KeyDown += txtFullName_KeyDown;
            // 
            // txtPosition
            // 
            txtPosition.Location = new Point(130, 57);
            txtPosition.Name = "txtPosition";
            txtPosition.Size = new Size(250, 23);
            txtPosition.TabIndex = 4;
            txtPosition.KeyDown += txtPosition_KeyDown;
            // 
            // cmbDepartment
            // 
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.FormattingEnabled = true;
            cmbDepartment.Location = new Point(130, 97);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(250, 23);
            cmbDepartment.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(130, 140);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(230, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // EmployeeEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(400, 185);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbDepartment);
            Controls.Add(txtPosition);
            Controls.Add(txtFullName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EmployeeEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование сотрудника";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}