using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class InstallSoftwareForm : Form
    {
        public int SelectedEquipmentId { get; private set; }
        public int SelectedSoftwareLicenseId { get; private set; }
        public DateTime? InstallationDate { get; private set; }
        public string InstallationPath { get; private set; }
        public string Notes { get; private set; }

        private EquipmentService _equipmentService;
        private SoftwareLicenseService _licenseService;
        private InstalledSoftwareService _installedService;

        public InstallSoftwareForm(
            EquipmentService equipmentService,
            SoftwareLicenseService licenseService,
            InstalledSoftwareService installedService)
        {
            _equipmentService = equipmentService;
            _licenseService = licenseService;
            _installedService = installedService;

            InitializeComponent();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            try
            {
                var equipment = _equipmentService.GetAll().ToList();
                if (equipment.Count == 0)
                {
                    MessageBox.Show("Нет доступного оборудования", "Информация");
                    Close();
                    return;
                }

                cmbEquipment.DataSource = equipment;
                cmbEquipment.DisplayMember = "DisplayInfo";
                cmbEquipment.ValueMember = "Id";
                cmbEquipment.SelectedIndex = 0;

                // Загружаем ПО для первого оборудования
                LoadAvailableSoftware();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки оборудования: {ex.Message}", "Ошибка");
            }
        }

        private void LoadAvailableSoftware()
        {
            try
            {
                if (cmbEquipment.SelectedItem is EquipmentDTO selectedEquipment)
                {
                    var availableSoftware = _installedService
                        .GetAvailableSoftwareForEquipment(selectedEquipment.Id)
                        .ToList();

                    if (availableSoftware.Count == 0)
                    {
                        lblSoftwareInfo.Text = "Нет доступного ПО для этого оборудования";
                        cmbSoftware.DataSource = null;
                        cmbSoftware.Enabled = false;
                    }
                    else
                    {
                        cmbSoftware.DataSource = availableSoftware;
                        cmbSoftware.DisplayMember = "DisplayInfo";
                        cmbSoftware.ValueMember = "Id";
                        cmbSoftware.Enabled = true;

                        lblSoftwareInfo.Text = $"Доступно ПО: {availableSoftware.Count}";

                        if (cmbSoftware.Items.Count > 0)
                            cmbSoftware.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ПО: {ex.Message}", "Ошибка");
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (cmbEquipment.SelectedItem is EquipmentDTO selectedEquipment)
            {
                SelectedEquipmentId = selectedEquipment.Id;
            }

            if (cmbSoftware.SelectedItem is SoftwareLicenseDTO selectedSoftware)
            {
                SelectedSoftwareLicenseId = selectedSoftware.Id;
            }

            InstallationDate = chkCustomDate.Checked ? dtpInstallationDate.Value : (DateTime?)null;
            InstallationPath = txtInstallationPath.Text.Trim();
            Notes = txtNotes.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            if (cmbEquipment.SelectedItem == null)
            {
                MessageBox.Show("Выберите оборудование", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbSoftware.SelectedItem == null || !cmbSoftware.Enabled)
            {
                MessageBox.Show("Нет доступного ПО для установки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbSoftware.SelectedItem is SoftwareLicenseDTO selectedSoftware)
            {
                if (selectedSoftware.AvailableCount <= 0)
                {
                    MessageBox.Show("Нет доступных лицензий для выбранного ПО", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (selectedSoftware.IsExpired)
                {
                    MessageBox.Show("Срок действия лицензии истек", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableSoftware();
        }

        private void chkCustomDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpInstallationDate.Enabled = chkCustomDate.Checked;
        }
    }
}