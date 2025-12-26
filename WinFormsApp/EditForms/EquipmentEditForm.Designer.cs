namespace WinFormsApp
{
    partial class EquipmentEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private TextBox txtInventoryNumber;
        private TextBox txtName;
        private ComboBox cmbEquipmentType;
        private TextBox txtSerialNumber;
        private ComboBox cmbEmployee;
        private DateTimePicker dtpRegistrationDate;
        private CheckBox chkPurchaseDate;
        private DateTimePicker dtpPurchaseDate;
        private CheckBox chkPurchasePrice;
        private TextBox txtPurchasePrice;
        private ComboBox cmbStatus;
        private TextBox txtLocation;
        private TextBox txtSpecifications;
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
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            txtInventoryNumber = new TextBox();
            txtName = new TextBox();
            cmbEquipmentType = new ComboBox();
            txtSerialNumber = new TextBox();
            cmbEmployee = new ComboBox();
            dtpRegistrationDate = new DateTimePicker();
            chkPurchaseDate = new CheckBox();
            dtpPurchaseDate = new DateTimePicker();
            chkPurchasePrice = new CheckBox();
            txtPurchasePrice = new TextBox();
            cmbStatus = new ComboBox();
            txtLocation = new TextBox();
            txtSpecifications = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 0;
            label1.Text = "Инвентарный №:*";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 50);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 1;
            label2.Text = "Название:*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 80);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 2;
            label3.Text = "Тип оборудования:*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 110);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 3;
            label4.Text = "Серийный №:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 140);
            label5.Name = "label5";
            label5.Size = new Size(94, 15);
            label5.TabIndex = 4;
            label5.Text = "Ответственный:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 170);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 5;
            label6.Text = "Дата учета:*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 200);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 6;
            label7.Text = "Дата покупки:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 230);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 7;
            label8.Text = "Цена покупки:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(20, 260);
            label9.Name = "label9";
            label9.Size = new Size(46, 15);
            label9.TabIndex = 8;
            label9.Text = "Статус:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 290);
            label10.Name = "label10";
            label10.Size = new Size(112, 15);
            label10.TabIndex = 9;
            label10.Text = "Местонахождение:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(20, 320);
            label11.Name = "label11";
            label11.Size = new Size(120, 15);
            label11.TabIndex = 10;
            label11.Text = "Технические хар-ки:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(388, 406);
            label12.Name = "label12";
            label12.Size = new Size(130, 15);
            label12.TabIndex = 11;
            label12.Text = "* - обязательные поля";
            // 
            // txtInventoryNumber
            // 
            txtInventoryNumber.Location = new Point(160, 17);
            txtInventoryNumber.Name = "txtInventoryNumber";
            txtInventoryNumber.Size = new Size(200, 23);
            txtInventoryNumber.TabIndex = 12;
            // 
            // txtName
            // 
            txtName.Location = new Point(160, 47);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 13;
            // 
            // cmbEquipmentType
            // 
            cmbEquipmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipmentType.FormattingEnabled = true;
            cmbEquipmentType.Location = new Point(160, 77);
            cmbEquipmentType.Name = "cmbEquipmentType";
            cmbEquipmentType.Size = new Size(200, 23);
            cmbEquipmentType.TabIndex = 14;
            // 
            // txtSerialNumber
            // 
            txtSerialNumber.Location = new Point(160, 107);
            txtSerialNumber.Name = "txtSerialNumber";
            txtSerialNumber.Size = new Size(200, 23);
            txtSerialNumber.TabIndex = 15;
            // 
            // cmbEmployee
            // 
            cmbEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmployee.FormattingEnabled = true;
            cmbEmployee.Location = new Point(160, 137);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new Size(250, 23);
            cmbEmployee.TabIndex = 16;
            // 
            // dtpRegistrationDate
            // 
            dtpRegistrationDate.Location = new Point(160, 167);
            dtpRegistrationDate.Name = "dtpRegistrationDate";
            dtpRegistrationDate.Size = new Size(200, 23);
            dtpRegistrationDate.TabIndex = 17;
            // 
            // chkPurchaseDate
            // 
            chkPurchaseDate.AutoSize = true;
            chkPurchaseDate.Location = new Point(160, 200);
            chkPurchaseDate.Name = "chkPurchaseDate";
            chkPurchaseDate.Size = new Size(15, 14);
            chkPurchaseDate.TabIndex = 18;
            chkPurchaseDate.UseVisualStyleBackColor = true;
            chkPurchaseDate.CheckedChanged += chkPurchaseDate_CheckedChanged;
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.Enabled = false;
            dtpPurchaseDate.Location = new Point(180, 197);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.Size = new Size(180, 23);
            dtpPurchaseDate.TabIndex = 19;
            // 
            // chkPurchasePrice
            // 
            chkPurchasePrice.AutoSize = true;
            chkPurchasePrice.Location = new Point(160, 230);
            chkPurchasePrice.Name = "chkPurchasePrice";
            chkPurchasePrice.Size = new Size(15, 14);
            chkPurchasePrice.TabIndex = 20;
            chkPurchasePrice.UseVisualStyleBackColor = true;
            chkPurchasePrice.CheckedChanged += chkPurchasePrice_CheckedChanged;
            // 
            // txtPurchasePrice
            // 
            txtPurchasePrice.Enabled = false;
            txtPurchasePrice.Location = new Point(180, 227);
            txtPurchasePrice.Name = "txtPurchasePrice";
            txtPurchasePrice.Size = new Size(100, 23);
            txtPurchasePrice.TabIndex = 21;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(160, 257);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(150, 23);
            cmbStatus.TabIndex = 22;
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(160, 287);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(200, 23);
            txtLocation.TabIndex = 23;
            // 
            // txtSpecifications
            // 
            txtSpecifications.Location = new Point(160, 317);
            txtSpecifications.Multiline = true;
            txtSpecifications.Name = "txtSpecifications";
            txtSpecifications.Size = new Size(300, 60);
            txtSpecifications.TabIndex = 24;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(160, 390);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 25;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(260, 390);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 26;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // EquipmentEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(530, 430);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtSpecifications);
            Controls.Add(txtLocation);
            Controls.Add(cmbStatus);
            Controls.Add(txtPurchasePrice);
            Controls.Add(chkPurchasePrice);
            Controls.Add(dtpPurchaseDate);
            Controls.Add(chkPurchaseDate);
            Controls.Add(dtpRegistrationDate);
            Controls.Add(cmbEmployee);
            Controls.Add(txtSerialNumber);
            Controls.Add(cmbEquipmentType);
            Controls.Add(txtName);
            Controls.Add(txtInventoryNumber);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
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
            Name = "EquipmentEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование оборудования";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}