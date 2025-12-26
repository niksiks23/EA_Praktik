
namespace WinFormsApp
{
    partial class EquipmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private TextBox txtSearch;
        private Label lblSearch;
        private Button btnReport;
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
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();

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
            dataGridView1.Location = new Point(0, 53);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(900, 497);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(lblSearch);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnRefresh);

            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(900, 53);
            panel1.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(380, 15);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 5;
            lblSearch.Text = "Поиск:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(430, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnAdd
            // 
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(10, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 25);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            // 
            // btnEdit
            // 
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Location = new Point(100, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 25);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(190, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 25);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(280, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 25);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ButtonHighlight;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 550);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(900, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(65, 17);
            lblStatus.Text = "Записей: 0";
            // 
            // EquipmentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 572);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "EquipmentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Оборудование";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            // 
            // btnReport
            // 
            //this.btnReport.Location = new System.Drawing.Point(780, 10);
            //this.btnReport.Name = "btnReport";
            //this.btnReport.Size = new System.Drawing.Size(100, 25);
            //this.btnReport.TabIndex = 7;
            //this.btnReport.Text = "Отчет";
            //this.btnReport.UseVisualStyleBackColor = true;
            //this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            //this.btnReport = new System.Windows.Forms.Button();
        }
    }
}