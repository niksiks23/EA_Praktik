namespace WinFormsApp
{
    partial class EquipmentHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btnRefresh;
        private Button btnViewByEquipment;
        private Button btnShowAll;
        private Button btnDelete;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;

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
            btnDelete = new Button();
            btnShowAll = new Button();
            btnViewByEquipment = new Button();
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
            dataGridView1.Location = new Point(0, 43);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(857, 431);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnShowAll);
            panel1.Controls.Add(btnViewByEquipment);
            panel1.Controls.Add(btnRefresh);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(857, 43);
            panel1.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(268, 9);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(69, 26);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(181, 9);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(69, 26);
            btnShowAll.TabIndex = 2;
            btnShowAll.Text = "Все";
            btnShowAll.UseVisualStyleBackColor = true;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnViewByEquipment
            // 
            btnViewByEquipment.FlatStyle = FlatStyle.Flat;
            btnViewByEquipment.Location = new Point(86, 9);
            btnViewByEquipment.Name = "btnViewByEquipment";
            btnViewByEquipment.Size = new Size(89, 26);
            btnViewByEquipment.TabIndex = 1;
            btnViewByEquipment.Text = "По оборуд.";
            btnViewByEquipment.UseVisualStyleBackColor = true;
            btnViewByEquipment.Click += btnViewByEquipment_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(9, 9);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(69, 26);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ButtonHighlight;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 474);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(857, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(62, 17);
            lblStatus.Text = "Записей: 0";
            // 
            // EquipmentHistoryForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(857, 496);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "EquipmentHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "История перемещений оборудования";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}