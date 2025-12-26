using BLL.DTOs;
using BLL.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class InstalledSoftwareForm : Form
    {
        private InstalledSoftwareService _installedService;
        private EquipmentService _equipmentService;
        private SoftwareLicenseService _licenseService;
        private BindingSource _bindingSource = new BindingSource();

        public InstalledSoftwareForm(
            InstalledSoftwareService installedService,
            EquipmentService equipmentService,
            SoftwareLicenseService licenseService)
        {
            _installedService = installedService;
            _equipmentService = equipmentService;
            _licenseService = licenseService;

            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var installations = _installedService.GetAll().ToList();
                _bindingSource.DataSource = installations;
                dataGridView1.DataSource = _bindingSource;
                UpdateStatusLabel(installations.Count, installations.Count(i => i.IsActive));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusLabel(int total, int active)
        {
            lblStatus.Text = $"Всего: {total} | Активных: {active} | Удаленных: {total - active}";
        }

        private void ConfigureDataGridView()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Equipment", "Оборудование");
                dataGridView1.Columns.Add("Software", "Программное обеспечение");
                dataGridView1.Columns.Add("InstallDate", "Дата установки");
                dataGridView1.Columns.Add("Status", "Статус");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var availableEquipment = _equipmentService.GetAll().ToList();
                if (availableEquipment.Count == 0)
                {
                    MessageBox.Show("Нет доступного оборудования для установки ПО", "Информация");
                    return;
                }

                var availableSoftware = _licenseService.GetAvailableForInstallation();
                if (availableSoftware.Count == 0)
                {
                    MessageBox.Show("Нет доступных лицензий для установки", "Информация");
                    return;
                }

                using (var installForm = new InstallSoftwareForm(_equipmentService, _licenseService, _installedService))
                {
                    if (installForm.ShowDialog() == DialogResult.OK)
                    {
                        _installedService.InstallSoftware(new InstalledSoftwareCreateDTO
                        {
                            EquipmentId = installForm.SelectedEquipmentId,
                            SoftwareLicenseId = installForm.SelectedSoftwareLicenseId,
                            InstallationDate = installForm.InstallationDate,
                            InstallationPath = installForm.InstallationPath,
                            Notes = installForm.Notes
                        });

                        MessageBox.Show("ПО успешно установлено", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите установку для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var installation = (InstalledSoftwareDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (!installation.IsActive)
            {
                MessageBox.Show("Это ПО уже было удалено ранее", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Удалить ПО '{installation.SoftwareName}' с оборудования '{installation.EquipmentName}'?\n\n" +
                               $"Инв. №: {installation.EquipmentInventoryNumber}",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _installedService.UninstallSoftware(installation.Id);

                    MessageBox.Show("ПО успешно удалено", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnViewByEquipment_Click(object sender, EventArgs e)
        {
            using (var selectForm = new EquipmentSelectForm(_equipmentService))
            {
                if (selectForm.ShowDialog() == DialogResult.OK && selectForm.SelectedEquipmentId.HasValue)
                {
                    try
                    {
                        var installations = _installedService.GetByEquipmentId(
                            selectForm.SelectedEquipmentId.Value).ToList();

                        _bindingSource.DataSource = installations;
                        dataGridView1.DataSource = _bindingSource;
                        UpdateStatusLabel(installations.Count, installations.Count(i => i.IsActive));

                        MessageBox.Show($"Найдено {installations.Count} установок для выбранного оборудования",
                            "Результаты фильтрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnShowActive_Click(object sender, EventArgs e)
        {
            try
            {
                var all = _installedService.GetAll().ToList();
                var active = all.Where(i => i.IsActive).ToList();

                _bindingSource.DataSource = active;
                dataGridView1.DataSource = _bindingSource;
                UpdateStatusLabel(active.Count, active.Count);

                MessageBox.Show($"Показаны только активные установки: {active.Count} из {all.Count}",
                    "Фильтр", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Показаны все установки", "Фильтр",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                var stats = _installedService.GetStatistics();

                string message = "=== СТАТИСТИКА УСТАНОВЛЕННОГО ПО ===\n\n";
                foreach (var stat in stats)
                {
                    if (stat.Key == "Популярное ПО")
                    {
                        message += "\nСАМОЕ ПОПУЛЯРНОЕ ПО:\n";
                        var popularList = (System.Collections.IEnumerable)stat.Value;
                        foreach (dynamic item in popularList)
                        {
                            message += $"- {item.SoftwareName}: {item.InstallCount} установок\n";
                        }
                    }
                    else
                    {
                        message += $"{stat.Key}: {stat.Value}\n";
                    }
                }

                MessageBox.Show(message, "Статистика",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения статистики: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLicenseReport_Click(object sender, EventArgs e)
        {
            try
            {
                var report = _installedService.GetLicenseUsageReport();

                string message = "=== ОТЧЕТ ПО ИСПОЛЬЗОВАНИЮ ЛИЦЕНЗИЙ ===\n\n";
                foreach (var item in report)
                {
                    var expiryInfo = item.ExpiryDate.HasValue ?
                        item.ExpiryDate.Value.ToString("dd.MM.yyyy") : "Бессрочно";

                    if (item.IsExpired)
                        expiryInfo += " (ИСТЕК!)";

                    message += $"ПО: {item.SoftwareName} v{item.Version}\n";
                    message += $"Лицензии: {item.UsedLicenses}/{item.TotalLicenses} (доступно: {item.AvailableLicenses})\n";
                    message += $"Срок: {expiryInfo}\n";
                    message += "---------------------------------\n";
                }

                MessageBox.Show(message, "Отчет по лицензиям",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка формирования отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchText = txtSearch.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadData();
                    return;
                }

                var allData = _installedService.GetAll().ToList();
                var filtered = allData.Where(i =>
                    i.EquipmentName.ToLower().Contains(searchText) ||
                    i.EquipmentInventoryNumber.ToLower().Contains(searchText) ||
                    i.SoftwareName.ToLower().Contains(searchText) ||
                    i.SoftwareVersion.ToLower().Contains(searchText) ||
                    i.InstallationPath.ToLower().Contains(searchText) ||
                    i.Notes.ToLower().Contains(searchText))
                    .ToList();

                _bindingSource.DataSource = filtered;
                dataGridView1.DataSource = _bindingSource;
                UpdateStatusLabel(filtered.Count, filtered.Count(i => i.IsActive));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}