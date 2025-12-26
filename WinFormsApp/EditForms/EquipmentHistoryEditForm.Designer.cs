namespace WinFormsApp
{
    partial class EquipmentHistoryEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cmbEquipment;
        private DateTimePicker dtpChangeDate;
        private ComboBox cmbOldEmployee;
        private ComboBox cmbNewEmployee;
        private TextBox txtReason;
        private TextBox txtNotes;
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
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cmbEquipment = new ComboBox();
            dtpChangeDate = new DateTimePicker();
            cmbOldEmployee = new ComboBox();
            cmbNewEmployee = new ComboBox();
            txtReason = new TextBox();
            txtNotes = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(150, 18);
            label1.Name = "label1";
            label1.Size = new Size(211, 19);
            label1.TabIndex = 0;
            label1.Text = "Добавление записи истории";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 60);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 1;
            label2.Text = "Оборудование:*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 2;
            label3.Text = "Дата изменения:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 140);
            label4.Name = "label4";
            label4.Size = new Size(122, 15);
            label4.TabIndex = 3;
            label4.Text = "Прежний сотрудник:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 180);
            label5.Name = "label5";
            label5.Size = new Size(108, 15);
            label5.TabIndex = 4;
            label5.Text = "Новый сотрудник:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 220);
            label6.Name = "label6";
            label6.Size = new Size(65, 15);
            label6.TabIndex = 5;
            label6.Text = "Причина:*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 260);
            label7.Name = "label7";
            label7.Size = new Size(81, 15);
            label7.TabIndex = 6;
            label7.Text = "Примечания:";
            // 
            // cmbEquipment
            // 
            cmbEquipment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipment.FormattingEnabled = true;
            cmbEquipment.Location = new Point(150, 57);
            cmbEquipment.Name = "cmbEquipment";
            cmbEquipment.Size = new Size(350, 23);
            cmbEquipment.TabIndex = 7;
            cmbEquipment.SelectedIndexChanged += cmbEquipment_SelectedIndexChanged;
            // 
            // dtpChangeDate
            // 
            dtpChangeDate.Location = new Point(150, 97);
            dtpChangeDate.Name = "dtpChangeDate";
            dtpChangeDate.Size = new Size(200, 23);
            dtpChangeDate.TabIndex = 8;
            // 
            // cmbOldEmployee
            // 
            cmbOldEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOldEmployee.FormattingEnabled = true;
            cmbOldEmployee.Location = new Point(150, 137);
            cmbOldEmployee.Name = "cmbOldEmployee";
            cmbOldEmployee.Size = new Size(250, 23);
            cmbOldEmployee.TabIndex = 9;
            // 
            // cmbNewEmployee
            // 
            cmbNewEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNewEmployee.FormattingEnabled = true;
            cmbNewEmployee.Location = new Point(150, 177);
            cmbNewEmployee.Name = "cmbNewEmployee";
            cmbNewEmployee.Size = new Size(250, 23);
            cmbNewEmployee.TabIndex = 10;
            // 
            // txtReason
            // 
            txtReason.Location = new Point(150, 217);
            txtReason.Name = "txtReason";
            txtReason.Size = new Size(300, 23);
            txtReason.TabIndex = 11;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(150, 257);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(350, 60);
            txtNotes.TabIndex = 12;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(150, 330);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 13;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(250, 330);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // EquipmentHistoryEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(520, 380);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtNotes);
            Controls.Add(txtReason);
            Controls.Add(cmbNewEmployee);
            Controls.Add(cmbOldEmployee);
            Controls.Add(dtpChangeDate);
            Controls.Add(cmbEquipment);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EquipmentHistoryEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование истории";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}