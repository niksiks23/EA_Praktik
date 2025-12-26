namespace WinFormsApp
{
    partial class SoftwareLicenseForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnShowExpiring;
        private Button btnShowAll;
        private Button btnExport;
        private Button btnInstallations;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblStats;
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
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnInstallations = new Button();
            btnExport = new Button();
            btnShowAll = new Button();
            btnShowExpiring = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnRefresh = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblStats = new ToolStripStatusLabel();
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
            dataGridView1.Size = new Size(1200, 568);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(lblSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(btnInstallations);
            panel1.Controls.Add(btnExport);
            panel1.Controls.Add(btnShowAll);
            panel1.Controls.Add(btnShowExpiring);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnRefresh);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 80);
            panel1.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(437, 30);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 9;
            lblSearch.Text = "Поиск:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(499, 27);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 8;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnInstallations
            // 
            btnInstallations.FlatStyle = FlatStyle.Flat;
            btnInstallations.Location = new Point(283, 45);
            btnInstallations.Name = "btnInstallations";
            btnInstallations.Size = new Size(120, 25);
            btnInstallations.TabIndex = 7;
            btnInstallations.Text = "Установки";
            btnInstallations.UseVisualStyleBackColor = true;
            btnInstallations.Click += btnInstallations_Click;
            // 
            // btnExport
            // 
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Location = new Point(283, 10);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(80, 25);
            btnExport.TabIndex = 6;
            btnExport.Text = "Экспорт";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(186, 45);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(80, 25);
            btnShowAll.TabIndex = 5;
            btnShowAll.Text = "Все";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnShowExpiring
            // 
            btnShowExpiring.FlatStyle = FlatStyle.Flat;
            btnShowExpiring.Location = new Point(186, 10);
            btnShowExpiring.Name = "btnShowExpiring";
            btnShowExpiring.Size = new Size(80, 25);
            btnShowExpiring.TabIndex = 4;
            btnShowExpiring.Text = "Истекают";
            btnShowExpiring.UseVisualStyleBackColor = true;
            btnShowExpiring.Click += btnShowExpiring_Click;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(100, 45);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 25);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Location = new Point(100, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 25);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(10, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 25);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(10, 45);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 25);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ButtonHighlight;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, lblStats });
            statusStrip1.Location = new Point(0, 648);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1200, 24);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(73, 19);
            lblStatus.Text = "Лицензий: 0";
            // 
            // lblStats
            // 
            lblStats.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            lblStats.BorderStyle = Border3DStyle.SunkenOuter;
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(122, 19);
            lblStats.Text = "toolStripStatusLabel1";
            // 
            // SoftwareLicenseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 672);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "SoftwareLicenseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Лицензии программного обеспечения";
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