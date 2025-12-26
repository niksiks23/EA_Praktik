using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class SoftwareLicenseEditForm : Form
    {
        // Свойства для доступа к данным
        public string SoftwareName { get; private set; }
        public string Publisher { get; private set; }
        public string Version { get; private set; }
        public string LicenseKey { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public DateTime? ExpiryDate { get; private set; }
        public int LicenseCount { get; private set; }
        public string LicenseType { get; private set; }
        public decimal? PurchasePrice { get; private set; }
        public string Notes { get; private set; }

        private SoftwareLicenseService _licenseService;
        private bool _isEditMode = false;

        // Конструктор для добавления новой лицензии
        public SoftwareLicenseEditForm(SoftwareLicenseService licenseService)
        {
            _licenseService = licenseService;
            InitializeComponent();
            LoadLicenseTypes();
            SetDefaultValues();
        }

        // Конструктор для редактирования существующей лицензии
        public SoftwareLicenseEditForm(SoftwareLicenseService licenseService, SoftwareLicenseDTO license)
            : this(licenseService)
        {
            _isEditMode = true;
            Text = "Редактирование лицензии ПО";

            // Заполняем поля данными
            txtSoftwareName.Text = license.SoftwareName;
            txtPublisher.Text = license.Publisher;
            txtVersion.Text = license.Version;
            txtLicenseKey.Text = license.LicenseKey;
            dtpPurchaseDate.Value = license.PurchaseDate;

            if (license.ExpiryDate.HasValue)
            {
                dtpExpiryDate.Value = license.ExpiryDate.Value;
                chkHasExpiryDate.Checked = true;
            }
            else
            {
                chkHasExpiryDate.Checked = false;
                dtpExpiryDate.Enabled = false;
            }

            nudLicenseCount.Value = license.LicenseCount;

            // Выбираем тип лицензии
            foreach (var item in cmbLicenseType.Items)
            {
                if (item.ToString() == license.LicenseType)
                {
                    cmbLicenseType.SelectedItem = item;
                    break;
                }
            }

            if (license.PurchasePrice.HasValue)
            {
                txtPurchasePrice.Text = license.PurchasePrice.Value.ToString("0.00");
                chkHasPurchasePrice.Checked = true;
            }
            else
            {
                chkHasPurchasePrice.Checked = false;
                txtPurchasePrice.Enabled = false;
            }

            txtNotes.Text = license.Notes;
        }

        private void LoadLicenseTypes()
        {
            try
            {
                var licenseTypes = _licenseService.GetLicenseTypes();
                cmbLicenseType.DataSource = licenseTypes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов лицензий: {ex.Message}", "Ошибка");
            }
        }

        private void SetDefaultValues()
        {
            dtpPurchaseDate.Value = DateTime.Now;
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            nudLicenseCount.Value = 1;
            cmbLicenseType.SelectedIndex = 0; // "Коммерческая"
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            // Сохраняем значения из формы
            SoftwareName = txtSoftwareName.Text.Trim();
            Publisher = txtPublisher.Text.Trim();
            Version = txtVersion.Text.Trim();
            LicenseKey = txtLicenseKey.Text.Trim();
            PurchaseDate = dtpPurchaseDate.Value;

            ExpiryDate = chkHasExpiryDate.Checked ? dtpExpiryDate.Value : (DateTime?)null;

            LicenseCount = (int)nudLicenseCount.Value;
            LicenseType = cmbLicenseType.SelectedItem?.ToString() ?? "Коммерческая";

            if (chkHasPurchasePrice.Checked && decimal.TryParse(txtPurchasePrice.Text, out decimal price))
            {
                PurchasePrice = price;
            }
            else
            {
                PurchasePrice = null;
            }

            Notes = txtNotes.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            // Проверка названия ПО
            if (string.IsNullOrWhiteSpace(txtSoftwareName.Text))
            {
                MessageBox.Show("Название ПО не может быть пустым", "Ошибка");
                txtSoftwareName.Focus();
                return false;
            }

            // Проверка издателя
            if (string.IsNullOrWhiteSpace(txtPublisher.Text))
            {
                MessageBox.Show("Издатель не может быть пустым", "Ошибка");
                txtPublisher.Focus();
                return false;
            }

            // Проверка количества лицензий
            if (nudLicenseCount.Value <= 0)
            {
                MessageBox.Show("Количество лицензий должно быть больше 0", "Ошибка");
                nudLicenseCount.Focus();
                return false;
            }

            // Проверка даты окончания (если указана)
            if (chkHasExpiryDate.Checked && dtpExpiryDate.Value < dtpPurchaseDate.Value)
            {
                MessageBox.Show("Дата окончания не может быть раньше даты покупки", "Ошибка");
                dtpExpiryDate.Focus();
                return false;
            }

            // Проверка цены (если указана)
            if (chkHasPurchasePrice.Checked)
            {
                if (!decimal.TryParse(txtPurchasePrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Укажите корректную цену", "Ошибка");
                    txtPurchasePrice.Focus();
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

        private void chkHasExpiryDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpExpiryDate.Enabled = chkHasExpiryDate.Checked;
            if (!chkHasExpiryDate.Checked)
            {
                dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            }
        }

        private void chkHasPurchasePrice_CheckedChanged(object sender, EventArgs e)
        {
            txtPurchasePrice.Enabled = chkHasPurchasePrice.Checked;
            if (!chkHasPurchasePrice.Checked)
            {
                txtPurchasePrice.Text = "";
            }
        }
    }
}