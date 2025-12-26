using BLL.DTOs;
using BLL.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class SoftwareLicenseForm : Form
    {
        private SoftwareLicenseService _licenseService;
        private BindingSource _bindingSource = new BindingSource();

        public SoftwareLicenseForm(SoftwareLicenseService licenseService)
        {
            _licenseService = licenseService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var licenses = _licenseService.GetAll().ToList();
                _bindingSource.DataSource = licenses;
                dataGridView1.DataSource = _bindingSource;

                // Настраиваем колонки
                ConfigureDataGridView();

                lblStatus.Text = $"Лицензий: {licenses.Count}";

                // Подсвечиваем просроченные и истекающие лицензии
                HighlightExpiringLicenses();

                // Обновляем статистику
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                // Настраиваем видимые колонки и заголовки
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["IsExpired"].Visible = false;
                dataGridView1.Columns["IsExpiringSoon"].Visible = false;

                //dataGridView1.Columns["SoftwareName"].HeaderText = "Название ПО";
                //dataGridView1.Columns["SoftwareName"].Width = 180;

                //dataGridView1.Columns["Publisher"].HeaderText = "Издатель";
                //dataGridView1.Columns["Publisher"].Width = 120;

                //dataGridView1.Columns["Version"].HeaderText = "Версия";
                //dataGridView1.Columns["Version"].Width = 80;

                //dataGridView1.Columns["LicenseKey"].HeaderText = "Ключ лицензии";
                //dataGridView1.Columns["LicenseKey"].Width = 150;

                //dataGridView1.Columns["PurchaseDate"].HeaderText = "Дата покупки";
                //dataGridView1.Columns["PurchaseDate"].Width = 100;
                //dataGridView1.Columns["PurchaseDate"].DefaultCellStyle.Format = "dd.MM.yyyy";

                //dataGridView1.Columns["ExpiryDate"].HeaderText = "Дата окончания";
                //dataGridView1.Columns["ExpiryDate"].Width = 100;
                //dataGridView1.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd.MM.yyyy";

                //dataGridView1.Columns["LicenseCount"].HeaderText = "Всего";
                //dataGridView1.Columns["LicenseCount"].Width = 60;

                //dataGridView1.Columns["InstalledCount"].HeaderText = "Установлено";
                //dataGridView1.Columns["InstalledCount"].Width = 80;

                //dataGridView1.Columns["AvailableCount"].HeaderText = "Доступно";
                //dataGridView1.Columns["AvailableCount"].Width = 80;

                //dataGridView1.Columns["LicenseType"].HeaderText = "Тип";
                //dataGridView1.Columns["LicenseType"].Width = 100;

                //dataGridView1.Columns["PurchasePrice"].HeaderText = "Цена";
                //dataGridView1.Columns["PurchasePrice"].Width = 80;
                //dataGridView1.Columns["PurchasePrice"].DefaultCellStyle.Format = "N2";

                //dataGridView1.Columns["Notes"].HeaderText = "Примечания";
                //dataGridView1.Columns["Notes"].Width = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка настройки DataGridView: {ex.Message}");
            }
        }

        private void HighlightExpiringLicenses()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DataBoundItem is SoftwareLicenseDTO license)
                {
                    if (license.IsExpired)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (license.IsExpiringSoon)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                    }
                    else if (license.AvailableCount <= 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var stats = _licenseService.GetStatistics();
                lblStats.Text = $"Всего: {stats["Всего лицензий"]} | " +
                               $"Установлено: {stats["Установлено"]}/{stats["Всего мест"]} | " +
                               $"Истекает: {stats["Истекает в течение месяца"]} | " +
                               $"Просрочено: {stats["Просрочено"]}";
            }
            catch
            {
                lblStats.Text = "Статистика недоступна";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new SoftwareLicenseEditForm(_licenseService))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _licenseService.Create(new SoftwareLicenseCreateDTO
                        {
                            SoftwareName = editForm.SoftwareName,
                            Publisher = editForm.Publisher,
                            Version = editForm.Version,
                            LicenseKey = editForm.LicenseKey,
                            PurchaseDate = editForm.PurchaseDate,
                            ExpiryDate = editForm.ExpiryDate,
                            LicenseCount = editForm.LicenseCount,
                            LicenseType = editForm.LicenseType,
                            PurchasePrice = editForm.PurchasePrice,
                            Notes = editForm.Notes
                        });

                        MessageBox.Show("Лицензия ПО успешно добавлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите лицензию для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var license = (SoftwareLicenseDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            using (var editForm = new SoftwareLicenseEditForm(_licenseService, license))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        license.SoftwareName = editForm.SoftwareName;
                        license.Publisher = editForm.Publisher;
                        license.Version = editForm.Version;
                        license.LicenseKey = editForm.LicenseKey;
                        license.PurchaseDate = editForm.PurchaseDate;
                        license.ExpiryDate = editForm.ExpiryDate;
                        license.LicenseCount = editForm.LicenseCount;
                        license.LicenseType = editForm.LicenseType;
                        license.PurchasePrice = editForm.PurchasePrice;
                        license.Notes = editForm.Notes;

                        _licenseService.Update(license);

                        MessageBox.Show("Данные лицензии обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите лицензию для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var license = (SoftwareLicenseDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Удалить лицензию '{license.SoftwareName}'?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _licenseService.Delete(license.Id);

                    MessageBox.Show("Лицензия удалена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnShowExpiring_Click(object sender, EventArgs e)
        {
            try
            {
                var expiringLicenses = _licenseService.GetExpiringSoon(30).ToList();
                _bindingSource.DataSource = expiringLicenses;
                dataGridView1.DataSource = _bindingSource;

                ConfigureDataGridView();
                HighlightExpiringLicenses();

                lblStatus.Text = $"Истекает в течение месяца: {expiringLicenses.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Excel файлы (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Экспорт лицензий";
                saveFileDialog.FileName = $"Лицензии_ПО_{DateTime.Now:yyyyMMdd}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (extension == ".csv")
                    {
                        ExportToCsv(saveFileDialog.FileName);
                    }
                    else if (extension == ".xlsx")
                    {
                        ExportToExcel(saveFileDialog.FileName);
                    }
                    else
                    {
                        ExportToCsv(saveFileDialog.FileName);
                    }

                    MessageBox.Show($"Данные экспортированы в файл:\n{saveFileDialog.FileName}",
                        "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка");
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // Заголовки
                writer.WriteLine("Название ПО;Издатель;Версия;Ключ лицензии;Дата покупки;Дата окончания;Тип;Кол-во;Установлено;Доступно;Цена;Примечания");

                // Данные
                var data = _bindingSource.DataSource as List<SoftwareLicenseDTO>;
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        writer.WriteLine($"\"{item.SoftwareName}\";" +
                                       $"\"{item.Publisher}\";" +
                                       $"\"{item.Version}\";" +
                                       $"\"{item.LicenseKey}\";" +
                                       $"\"{item.PurchaseDate:dd.MM.yyyy}\";" +
                                       $"\"{(item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd.MM.yyyy") : "")}\";" +
                                       $"\"{item.LicenseType}\";" +
                                       $"{item.LicenseCount};" +
                                       $"{item.InstalledCount};" +
                                       $"{item.AvailableCount};" +
                                       $"{(item.PurchasePrice.HasValue ? item.PurchasePrice.Value.ToString("N2") : "")};" +
                                       $"\"{item.Notes}\"");
                    }
                }
            }
        }

        private void ExportToExcel(string filePath)
        {
            MessageBox.Show("Экспорт в Excel находится в разработке. Используйте CSV экспорт.",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var searchText = txtSearch.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadData();
                }
                else
                {
                    var allLicenses = _licenseService.GetAll();
                    var filtered = allLicenses
                        .Where(l => l.SoftwareName.ToLower().Contains(searchText) ||
                                   l.Publisher.ToLower().Contains(searchText) ||
                                   l.LicenseKey.ToLower().Contains(searchText) ||
                                   l.Version.ToLower().Contains(searchText))
                        .ToList();

                    _bindingSource.DataSource = filtered;
                    dataGridView1.DataSource = _bindingSource;

                    ConfigureDataGridView();
                    HighlightExpiringLicenses();

                    lblStatus.Text = $"Найдено: {filtered.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка");
            }
        }

        private void btnInstallations_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите лицензию для просмотра установок", "Информация");
                return;
            }

            var license = (SoftwareLicenseDTO)dataGridView1.SelectedRows[0].DataBoundItem;

            MessageBox.Show($"Для лицензии '{license.SoftwareName}':\n\n" +
                          $"Всего лицензий: {license.LicenseCount}\n" +
                          $"Установлено: {license.InstalledCount}\n" +
                          $"Доступно: {license.AvailableCount}\n\n" +
                          $"Модуль управления установками находится в разработке.",
                          "Информация об установках",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}