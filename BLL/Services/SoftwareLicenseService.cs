using BLL.DTOs;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class SoftwareLicenseService
    {
        private readonly AppDbContext _context;

        public SoftwareLicenseService(AppDbContext context)
        {
            _context = context;
        }

        public List<SoftwareLicenseDTO> GetAll()
        {
            try
            {
                var licenses = _context.SoftwareLicenses
                    .AsNoTracking()
                    .Select(sl => new SoftwareLicenseDTO
                    {
                        Id = sl.Id,
                        SoftwareName = sl.SoftwareName,
                        Publisher = sl.Publisher,
                        Version = sl.Version,
                        LicenseKey = sl.LicenseKey,
                        PurchaseDate = sl.PurchaseDate,
                        ExpiryDate = sl.ExpiryDate,
                        LicenseCount = sl.LicenseCount,
                        LicenseType = sl.LicenseType,
                        PurchasePrice = sl.PurchasePrice,
                        Notes = sl.Notes
                    })
                    .OrderBy(sl => sl.SoftwareName)
                    .ToList();

                // Вычисляем дополнительные поля
                foreach (var license in licenses)
                {
                    CalculateLicenseStats(license);
                }

                return licenses;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении лицензий ПО: {ex.Message}", ex);
            }
        }

        public SoftwareLicenseDTO GetById(int id)
        {
            var license = _context.SoftwareLicenses
                .AsNoTracking()
                .Where(sl => sl.Id == id)
                .Select(sl => new SoftwareLicenseDTO
                {
                    Id = sl.Id,
                    SoftwareName = sl.SoftwareName,
                    Publisher = sl.Publisher,
                    Version = sl.Version,
                    LicenseKey = sl.LicenseKey,
                    PurchaseDate = sl.PurchaseDate,
                    ExpiryDate = sl.ExpiryDate,
                    LicenseCount = sl.LicenseCount,
                    LicenseType = sl.LicenseType,
                    PurchasePrice = sl.PurchasePrice,
                    Notes = sl.Notes
                })
                .FirstOrDefault();

            if (license != null)
            {
                CalculateLicenseStats(license);
            }

            return license;
        }

        public List<SoftwareLicenseDTO> GetExpiringSoon(int days = 30)
        {
            var expirationDate = DateTime.Now.AddDays(days);

            var licenses = _context.SoftwareLicenses
                .Where(sl => sl.ExpiryDate != null && sl.ExpiryDate <= expirationDate)
                .AsNoTracking()
                .Select(sl => new SoftwareLicenseDTO
                {
                    Id = sl.Id,
                    SoftwareName = sl.SoftwareName,
                    Publisher = sl.Publisher,
                    Version = sl.Version,
                    LicenseKey = sl.LicenseKey,
                    PurchaseDate = sl.PurchaseDate,
                    ExpiryDate = sl.ExpiryDate,
                    LicenseCount = sl.LicenseCount,
                    LicenseType = sl.LicenseType,
                    PurchasePrice = sl.PurchasePrice,
                    Notes = sl.Notes
                })
                .ToList();

            foreach (var license in licenses)
            {
                CalculateLicenseStats(license);
            }

            return licenses;
        }

        public List<SoftwareLicenseDTO> GetAvailableForInstallation()
        {
            var licenses = _context.SoftwareLicenses
                .AsNoTracking()
                .Select(sl => new SoftwareLicenseDTO
                {
                    Id = sl.Id,
                    SoftwareName = sl.SoftwareName,
                    Publisher = sl.Publisher,
                    Version = sl.Version,
                    LicenseKey = sl.LicenseKey,
                    PurchaseDate = sl.PurchaseDate,
                    ExpiryDate = sl.ExpiryDate,
                    LicenseCount = sl.LicenseCount,
                    LicenseType = sl.LicenseType,
                    PurchasePrice = sl.PurchasePrice,
                    Notes = sl.Notes
                })
                .ToList();

            foreach (var license in licenses)
            {
                CalculateLicenseStats(license);
            }

            // Возвращаем только лицензии с доступными установками
            return licenses.Where(l => l.AvailableCount > 0 && !l.IsExpired).ToList();
        }

        private void CalculateLicenseStats(SoftwareLicenseDTO license)
        {
            // Количество установок
            license.InstalledCount = _context.InstalledSoftware
                .Count(isw => isw.SoftwareLicenseId == license.Id && isw.UninstallationDate == null);

            license.AvailableCount = license.LicenseCount - license.InstalledCount;

            // Проверка срока действия
            license.IsExpired = license.ExpiryDate.HasValue && license.ExpiryDate.Value < DateTime.Now;

            // Проверка скорого истечения срока (30 дней)
            license.IsExpiringSoon = license.ExpiryDate.HasValue &&
                                   license.ExpiryDate.Value > DateTime.Now &&
                                   license.ExpiryDate.Value <= DateTime.Now.AddDays(30);
        }

        public void Create(SoftwareLicenseCreateDTO dto)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(dto.SoftwareName))
                    throw new ArgumentException("Название ПО не может быть пустым");

                if (string.IsNullOrWhiteSpace(dto.Publisher))
                    throw new ArgumentException("Издатель не может быть пустым");

                if (dto.LicenseCount <= 0)
                    throw new ArgumentException("Количество лицензий должно быть больше 0");

                if (dto.ExpiryDate.HasValue && dto.ExpiryDate.Value < dto.PurchaseDate)
                    throw new ArgumentException("Дата окончания не может быть раньше даты покупки");

                var license = new SoftwareLicense
                {
                    SoftwareName = dto.SoftwareName.Trim(),
                    Publisher = dto.Publisher.Trim(),
                    Version = dto.Version?.Trim() ?? "",
                    LicenseKey = dto.LicenseKey?.Trim() ?? "",
                    PurchaseDate = dto.PurchaseDate,
                    ExpiryDate = dto.ExpiryDate,
                    LicenseCount = dto.LicenseCount,
                    LicenseType = dto.LicenseType?.Trim() ?? "Коммерческая",
                    PurchasePrice = dto.PurchasePrice,
                    Notes = dto.Notes?.Trim() ?? ""
                };

                _context.SoftwareLicenses.Add(license);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка создания лицензии: {ex.Message}", ex);
            }
        }

        public void Update(SoftwareLicenseDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.SoftwareName))
                    throw new ArgumentException("Название ПО не может быть пустым");

                if (string.IsNullOrWhiteSpace(dto.Publisher))
                    throw new ArgumentException("Издатель не может быть пустым");

                var license = _context.SoftwareLicenses.Find(dto.Id);
                if (license == null)
                    throw new ArgumentException("Лицензия не найдена");

                // Проверяем, что новое количество лицензий не меньше уже установленных
                var installedCount = _context.InstalledSoftware
                    .Count(isw => isw.SoftwareLicenseId == license.Id && isw.UninstallationDate == null);

                if (dto.LicenseCount < installedCount)
                    throw new ArgumentException($"Нельзя уменьшить количество лицензий до {dto.LicenseCount}. " +
                        $"Уже установлено: {installedCount}");

                if (dto.ExpiryDate.HasValue && dto.ExpiryDate.Value < dto.PurchaseDate)
                    throw new ArgumentException("Дата окончания не может быть раньше даты покупки");

                license.SoftwareName = dto.SoftwareName.Trim();
                license.Publisher = dto.Publisher.Trim();
                license.Version = dto.Version?.Trim() ?? "";
                license.LicenseKey = dto.LicenseKey?.Trim() ?? "";
                license.PurchaseDate = dto.PurchaseDate;
                license.ExpiryDate = dto.ExpiryDate;
                license.LicenseCount = dto.LicenseCount;
                license.LicenseType = dto.LicenseType?.Trim() ?? "Коммерческая";
                license.PurchasePrice = dto.PurchasePrice;
                license.Notes = dto.Notes?.Trim() ?? "";

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка обновления лицензии: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            var license = _context.SoftwareLicenses
                .Include(sl => sl.InstalledSoftware)
                .FirstOrDefault(sl => sl.Id == id);

            if (license == null) return;

            // Проверяем, нет ли установок ПО
            if (license.InstalledSoftware.Any(isw => isw.UninstallationDate == null))
                throw new InvalidOperationException("Нельзя удалить лицензию с активными установками ПО");

            _context.SoftwareLicenses.Remove(license);
            _context.SaveChanges();
        }

        public List<string> GetLicenseTypes()
        {
            return new List<string>
            {
                "Коммерческая",
                "Бесплатная",
                "Пробная",
                "Открытая",
                "Академическая",
                "Корпоративная",
                "Подписка"
            };
        }

        public Dictionary<string, int> GetStatistics()
        {
            var stats = new Dictionary<string, int>();

            stats["Всего лицензий"] = _context.SoftwareLicenses.Count();
            stats["Истекает в течение месяца"] = _context.SoftwareLicenses
                .Count(sl => sl.ExpiryDate != null &&
                           sl.ExpiryDate > DateTime.Now &&
                           sl.ExpiryDate <= DateTime.Now.AddDays(30));
            stats["Просрочено"] = _context.SoftwareLicenses
                .Count(sl => sl.ExpiryDate != null && sl.ExpiryDate < DateTime.Now);

            // Общее количество лицензий
            stats["Всего мест"] = _context.SoftwareLicenses.Sum(sl => sl.LicenseCount);

            // Установлено лицензий
            stats["Установлено"] = _context.InstalledSoftware
                .Count(isw => isw.UninstallationDate == null);

            stats["Доступно"] = stats["Всего мест"] - stats["Установлено"];

            return stats;
        }
    }
}