namespace WinFormsApp
{
    partial class InstalledSoftwareForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnAdd;
        private Button btnUninstall;
        private Button btnRefresh;
        private Button btnViewByEquipment;
        private Button btnShowActive;
        private Button btnShowAll;
        private Button btnStatistics;
        private Button btnLicenseReport;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TextBox txtSearch;
        private Label lblSearch;

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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            btnLicenseReport = new Button();
            btnStatistics = new Button();
            btnShowAll = new Button();
            btnShowActive = new Button();
            btnViewByEquipment = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnUninstall = new Button();
            btnAdd = new Button();
            btnRefresh = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 80);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1000, 520);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(btnLicenseReport);
            panel1.Controls.Add(btnStatistics);
            panel1.Controls.Add(btnShowAll);
            panel1.Controls.Add(btnShowActive);
            panel1.Controls.Add(btnViewByEquipment);
            panel1.Controls.Add(lblSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(btnUninstall);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnRefresh);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 80);
            panel1.TabIndex = 1;
            // 
            // btnLicenseReport
            // 
            btnLicenseReport.FlatStyle = FlatStyle.Flat;
            btnLicenseReport.Location = new Point(750, 45);
            btnLicenseReport.Name = "btnLicenseReport";
            btnLicenseReport.Size = new Size(131, 25);
            btnLicenseReport.TabIndex = 9;
            btnLicenseReport.Text = "Отчет по лицензиям";
            btnLicenseReport.UseVisualStyleBackColor = true;
            btnLicenseReport.Click += btnLicenseReport_Click;
            // 
            // btnStatistics
            // 
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Location = new Point(666, 45);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(78, 25);
            btnStatistics.TabIndex = 8;
            btnStatistics.Text = "Статистика";
            btnStatistics.UseVisualStyleBackColor = true;
            btnStatistics.Click += btnStatistics_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(596, 45);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(64, 25);
            btnShowAll.TabIndex = 7;
            btnShowAll.Text = "Все";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnShowActive
            // 
            btnShowActive.FlatStyle = FlatStyle.Flat;
            btnShowActive.Location = new Point(478, 45);
            btnShowActive.Name = "btnShowActive";
            btnShowActive.Size = new Size(112, 25);
            btnShowActive.TabIndex = 6;
            btnShowActive.Text = "Только активные";
            btnShowActive.UseVisualStyleBackColor = true;
            btnShowActive.Click += btnShowActive_Click;
            // 
            // btnViewByEquipment
            // 
            btnViewByEquipment.FlatStyle = FlatStyle.Flat;
            btnViewByEquipment.Location = new Point(350, 45);
            btnViewByEquipment.Name = "btnViewByEquipment";
            btnViewByEquipment.Size = new Size(122, 25);
            btnViewByEquipment.TabIndex = 5;
            btnViewByEquipment.Text = "По оборудованию";
            btnViewByEquipment.UseVisualStyleBackColor = true;
            btnViewByEquipment.Click += btnViewByEquipment_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(10, 50);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 4;
            lblSearch.Text = "Поиск:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(60, 47);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(280, 23);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnUninstall
            // 
            btnUninstall.FlatStyle = FlatStyle.Flat;
            btnUninstall.Location = new Point(100, 10);
            btnUninstall.Name = "btnUninstall";
            btnUninstall.Size = new Size(80, 25);
            btnUninstall.TabIndex = 2;
            btnUninstall.Text = "Удалить";
            btnUninstall.UseVisualStyleBackColor = true;
            btnUninstall.Click += btnUninstall_Click;
            // 
            // btnAdd
            // 
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(10, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 25);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Установить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(190, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 25);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ButtonHighlight;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 600);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1000, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(65, 17);
            lblStatus.Text = "Записей: 0";
            // 
            // InstalledSoftwareForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 622);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "InstalledSoftwareForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Установленное ПО";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}