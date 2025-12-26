namespace WinFormsApp
{
    partial class SoftwareLicenseEditForm
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
        private TextBox txtSoftwareName;
        private TextBox txtPublisher;
        private TextBox txtVersion;
        private TextBox txtLicenseKey;
        private DateTimePicker dtpPurchaseDate;
        private CheckBox chkHasExpiryDate;
        private DateTimePicker dtpExpiryDate;
        private NumericUpDown nudLicenseCount;
        private ComboBox cmbLicenseType;
        private CheckBox chkHasPurchasePrice;
        private TextBox txtPurchasePrice;
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
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            txtSoftwareName = new TextBox();
            txtPublisher = new TextBox();
            txtVersion = new TextBox();
            txtLicenseKey = new TextBox();
            dtpPurchaseDate = new DateTimePicker();
            chkHasExpiryDate = new CheckBox();
            dtpExpiryDate = new DateTimePicker();
            nudLicenseCount = new NumericUpDown();
            cmbLicenseType = new ComboBox();
            chkHasPurchasePrice = new CheckBox();
            txtPurchasePrice = new TextBox();
            txtNotes = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)nudLicenseCount).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 0;
            label1.Text = "Название ПО:*";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 60);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 1;
            label2.Text = "Издатель:*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 100);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 2;
            label3.Text = "Версия:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 140);
            label4.Name = "label4";
            label4.Size = new Size(97, 15);
            label4.TabIndex = 3;
            label4.Text = "Ключ лицензии:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 180);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 4;
            label5.Text = "Дата покупки:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 220);
            label6.Name = "label6";
            label6.Size = new Size(98, 15);
            label6.TabIndex = 5;
            label6.Text = "Дата окончания:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 260);
            label7.Name = "label7";
            label7.Size = new Size(109, 15);
            label7.TabIndex = 6;
            label7.Text = "Количество мест:*";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 300);
            label8.Name = "label8";
            label8.Size = new Size(86, 15);
            label8.TabIndex = 7;
            label8.Text = "Тип лицензии:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(20, 340);
            label9.Name = "label9";
            label9.Size = new Size(87, 15);
            label9.TabIndex = 8;
            label9.Text = "Цена покупки:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 380);
            label10.Name = "label10";
            label10.Size = new Size(81, 15);
            label10.TabIndex = 9;
            label10.Text = "Примечания:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(388, 458);
            label11.Name = "label11";
            label11.Size = new Size(130, 15);
            label11.TabIndex = 10;
            label11.Text = "* - обязательные поля";
            // 
            // txtSoftwareName
            // 
            txtSoftwareName.Location = new Point(150, 17);
            txtSoftwareName.Name = "txtSoftwareName";
            txtSoftwareName.Size = new Size(300, 23);
            txtSoftwareName.TabIndex = 11;
            // 
            // txtPublisher
            // 
            txtPublisher.Location = new Point(150, 57);
            txtPublisher.Name = "txtPublisher";
            txtPublisher.Size = new Size(250, 23);
            txtPublisher.TabIndex = 12;
            // 
            // txtVersion
            // 
            txtVersion.Location = new Point(150, 97);
            txtVersion.Name = "txtVersion";
            txtVersion.Size = new Size(100, 23);
            txtVersion.TabIndex = 13;
            // 
            // txtLicenseKey
            // 
            txtLicenseKey.Location = new Point(150, 137);
            txtLicenseKey.Name = "txtLicenseKey";
            txtLicenseKey.Size = new Size(250, 23);
            txtLicenseKey.TabIndex = 14;
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.Location = new Point(150, 177);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.Size = new Size(150, 23);
            dtpPurchaseDate.TabIndex = 15;
            // 
            // chkHasExpiryDate
            // 
            chkHasExpiryDate.AutoSize = true;
            chkHasExpiryDate.Location = new Point(150, 220);
            chkHasExpiryDate.Name = "chkHasExpiryDate";
            chkHasExpiryDate.Size = new Size(15, 14);
            chkHasExpiryDate.TabIndex = 16;
            chkHasExpiryDate.UseVisualStyleBackColor = true;
            chkHasExpiryDate.CheckedChanged += chkHasExpiryDate_CheckedChanged;
            // 
            // dtpExpiryDate
            // 
            dtpExpiryDate.Enabled = false;
            dtpExpiryDate.Location = new Point(170, 217);
            dtpExpiryDate.Name = "dtpExpiryDate";
            dtpExpiryDate.Size = new Size(150, 23);
            dtpExpiryDate.TabIndex = 17;
            // 
            // nudLicenseCount
            // 
            nudLicenseCount.Location = new Point(150, 257);
            nudLicenseCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLicenseCount.Name = "nudLicenseCount";
            nudLicenseCount.Size = new Size(80, 23);
            nudLicenseCount.TabIndex = 18;
            nudLicenseCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cmbLicenseType
            // 
            cmbLicenseType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLicenseType.FormattingEnabled = true;
            cmbLicenseType.Location = new Point(150, 297);
            cmbLicenseType.Name = "cmbLicenseType";
            cmbLicenseType.Size = new Size(150, 23);
            cmbLicenseType.TabIndex = 19;
            // 
            // chkHasPurchasePrice
            // 
            chkHasPurchasePrice.AutoSize = true;
            chkHasPurchasePrice.Location = new Point(150, 340);
            chkHasPurchasePrice.Name = "chkHasPurchasePrice";
            chkHasPurchasePrice.Size = new Size(15, 14);
            chkHasPurchasePrice.TabIndex = 20;
            chkHasPurchasePrice.UseVisualStyleBackColor = true;
            chkHasPurchasePrice.CheckedChanged += chkHasPurchasePrice_CheckedChanged;
            // 
            // txtPurchasePrice
            // 
            txtPurchasePrice.Enabled = false;
            txtPurchasePrice.Location = new Point(170, 337);
            txtPurchasePrice.Name = "txtPurchasePrice";
            txtPurchasePrice.Size = new Size(100, 23);
            txtPurchasePrice.TabIndex = 21;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(150, 377);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(350, 60);
            txtNotes.TabIndex = 22;
            // 
            // btnSave
            // 
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(150, 450);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 23;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(250, 450);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 24;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SoftwareLicenseEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(530, 490);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtNotes);
            Controls.Add(txtPurchasePrice);
            Controls.Add(chkHasPurchasePrice);
            Controls.Add(cmbLicenseType);
            Controls.Add(nudLicenseCount);
            Controls.Add(dtpExpiryDate);
            Controls.Add(chkHasExpiryDate);
            Controls.Add(dtpPurchaseDate);
            Controls.Add(txtLicenseKey);
            Controls.Add(txtVersion);
            Controls.Add(txtPublisher);
            Controls.Add(txtSoftwareName);
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
            Name = "SoftwareLicenseEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Добавление лицензии ПО";
            ((System.ComponentModel.ISupportInitialize)nudLicenseCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}