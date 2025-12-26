namespace WinFormsApp
{
    partial class InstallSoftwareForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cmbEquipment;
        private ComboBox cmbSoftware;
        private CheckBox chkCustomDate;
        private DateTimePicker dtpInstallationDate;
        private TextBox txtInstallationPath;
        private TextBox txtNotes;
        private Button btnInstall;
        private Button btnCancel;
        private Label lblSoftwareInfo;

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
            cmbEquipment = new ComboBox();
            cmbSoftware = new ComboBox();
            chkCustomDate = new CheckBox();
            dtpInstallationDate = new DateTimePicker();
            txtInstallationPath = new TextBox();
            txtNotes = new TextBox();
            btnInstall = new Button();
            btnCancel = new Button();
            lblSoftwareInfo = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(170, 19);
            label1.Name = "label1";
            label1.Size = new Size(189, 19);
            label1.TabIndex = 0;
            label1.Text = "Установка программного";
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
            label3.Size = new Size(126, 15);
            label3.TabIndex = 2;
            label3.Text = "Программное обес.:*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 140);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 3;
            label4.Text = "Дата установки:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 180);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 4;
            label5.Text = "Путь установки:";
            // 
            // cmbEquipment
            // 
            cmbEquipment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipment.FormattingEnabled = true;
            cmbEquipment.Location = new Point(150, 57);
            cmbEquipment.Name = "cmbEquipment";
            cmbEquipment.Size = new Size(350, 23);
            cmbEquipment.TabIndex = 5;
            cmbEquipment.SelectedIndexChanged += cmbEquipment_SelectedIndexChanged;
            // 
            // cmbSoftware
            // 
            cmbSoftware.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSoftware.FormattingEnabled = true;
            cmbSoftware.Location = new Point(150, 97);
            cmbSoftware.Name = "cmbSoftware";
            cmbSoftware.Size = new Size(350, 23);
            cmbSoftware.TabIndex = 6;
            // 
            // chkCustomDate
            // 
            chkCustomDate.AutoSize = true;
            chkCustomDate.Location = new Point(150, 140);
            chkCustomDate.Name = "chkCustomDate";
            chkCustomDate.Size = new Size(93, 19);
            chkCustomDate.TabIndex = 7;
            chkCustomDate.Text = "Указать дату";
            chkCustomDate.UseVisualStyleBackColor = true;
            chkCustomDate.CheckedChanged += chkCustomDate_CheckedChanged;
            // 
            // dtpInstallationDate
            // 
            dtpInstallationDate.Enabled = false;
            dtpInstallationDate.Location = new Point(260, 137);
            dtpInstallationDate.Name = "dtpInstallationDate";
            dtpInstallationDate.Size = new Size(200, 23);
            dtpInstallationDate.TabIndex = 8;
            // 
            // txtInstallationPath
            // 
            txtInstallationPath.Location = new Point(150, 177);
            txtInstallationPath.Name = "txtInstallationPath";
            txtInstallationPath.Size = new Size(350, 23);
            txtInstallationPath.TabIndex = 9;
            txtInstallationPath.Text = "C:\\Program Files\\";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(150, 217);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Примечания (необязательно)";
            txtNotes.Size = new Size(350, 60);
            txtNotes.TabIndex = 10;
            // 
            // btnInstall
            // 
            btnInstall.FlatStyle = FlatStyle.Flat;
            btnInstall.Location = new Point(150, 290);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(90, 30);
            btnInstall.TabIndex = 11;
            btnInstall.Text = "Установить";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(250, 290);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblSoftwareInfo
            // 
            lblSoftwareInfo.AutoSize = true;
            lblSoftwareInfo.ForeColor = Color.Blue;
            lblSoftwareInfo.Location = new Point(368, 316);
            lblSoftwareInfo.Name = "lblSoftwareInfo";
            lblSoftwareInfo.Size = new Size(140, 15);
            lblSoftwareInfo.TabIndex = 13;
            lblSoftwareInfo.Text = "Загрузка информации...";
            // 
            // InstallSoftwareForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(520, 340);
            Controls.Add(lblSoftwareInfo);
            Controls.Add(btnCancel);
            Controls.Add(btnInstall);
            Controls.Add(txtNotes);
            Controls.Add(txtInstallationPath);
            Controls.Add(dtpInstallationDate);
            Controls.Add(chkCustomDate);
            Controls.Add(cmbSoftware);
            Controls.Add(cmbEquipment);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InstallSoftwareForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Установка программного обеспечения";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}