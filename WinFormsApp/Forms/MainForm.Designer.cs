namespace WinFormsApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuExit;
        private ToolStripMenuItem menuData;
        private ToolStripMenuItem menuDepartments;
        private ToolStripMenuItem menuEmployees;
        private ToolStripMenuItem menuEquipment;
        private ToolStripMenuItem menuHistory;

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
            menuStrip1 = new MenuStrip();
            menuFile = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            menuData = new ToolStripMenuItem();
            menuDepartments = new ToolStripMenuItem();
            menuEmployees = new ToolStripMenuItem();
            menuEquipment = new ToolStripMenuItem();
            menuHistory = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonHighlight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuFile, menuData, menuHistory });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1089, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(48, 20);
            menuFile.Text = "Файл";
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(180, 22);
            menuExit.Text = "Выход";
            menuExit.Click += menuExit_Click;
            // 
            // menuData
            // 
            menuData.DropDownItems.AddRange(new ToolStripItem[] { menuDepartments, menuEmployees, menuEquipment });
            menuData.Name = "menuData";
            menuData.Size = new Size(62, 20);
            menuData.Text = "Данные";
            // 
            // menuDepartments
            // 
            menuDepartments.Name = "menuDepartments";
            menuDepartments.Size = new Size(180, 22);
            menuDepartments.Text = "Подразделения";
            menuDepartments.Click += menuDepartments_Click;
            // 
            // menuEmployees
            // 
            menuEmployees.Name = "menuEmployees";
            menuEmployees.Size = new Size(180, 22);
            menuEmployees.Text = "Сотрудники";
            menuEmployees.Click += menuEmployees_Click;
            // 
            // menuEquipment
            // 
            menuEquipment.Name = "menuEquipment";
            menuEquipment.Size = new Size(180, 22);
            menuEquipment.Text = "Оборудование";
            menuEquipment.Click += menuEquipment_Click;
            // 
            // menuHistory
            // 
            menuHistory.Name = "menuHistory";
            menuHistory.Size = new Size(66, 20);
            menuHistory.Text = "История";
            menuHistory.Click += menuHistory_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(1089, 521);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Учет оборудования";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}