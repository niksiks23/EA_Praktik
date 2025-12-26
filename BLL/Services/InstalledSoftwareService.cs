using BLL.DTOs;
using BLL.Interfaces;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class InstalledSoftwareService : IInstalledSoftwareService
    {
        private readonly AppDbContext _context;

        public InstalledSoftwareService(AppDbContext context)
        {
            _context = context;
        }

        //Получить все записи об установленном ПО
        public List<InstalledSoftwareDTO> GetAll()
        {
            try
            {
                return _context.InstalledSoftware
                    .Include(isw => isw.Equipment)
                    .Include(isw => isw.SoftwareLicense)
                    .AsNoTracking()
                    .Select(isw => new InstalledSoftwareDTO
                    {
                        Id = isw.Id,
                        EquipmentId = isw.EquipmentId,
                        EquipmentName = isw.Equipment.Name,
                        EquipmentInventoryNumber = isw.Equipment.InventoryNumber,
                        SoftwareLicenseId = isw.SoftwareLicenseId,
                        SoftwareName = isw.SoftwareLicense.SoftwareName,
                        SoftwareVersion = isw.SoftwareLicense.Version,
                        InstallationDate = isw.InstallationDate,
                        UninstallationDate = isw.UninstallationDate,
                        InstallationPath = isw.InstallationPath,
                        Notes = isw.Notes
                    })
                    .OrderByDescending(isw => isw.InstallationDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении установленного ПО: {ex.Message}", ex);
            }
        }

        //Получить запись по ID
        public InstalledSoftwareDTO GetById(int id)
        {
            return _context.InstalledSoftware
                .Include(isw => isw.Equipment)
                .Include(isw => isw.SoftwareLicense)
                .AsNoTracking()
                .Where(isw => isw.Id == id)
                .Select(isw => new InstalledSoftwareDTO
                {
                    Id = isw.Id,
                    EquipmentId = isw.EquipmentId,
                    EquipmentName = isw.Equipment.Name,
                    EquipmentInventoryNumber = isw.Equipment.InventoryNumber,
                    SoftwareLicenseId = isw.SoftwareLicenseId,
                    SoftwareName = isw.SoftwareLicense.SoftwareName,
                    SoftwareVersion = isw.SoftwareLicense.Version,
                    InstallationDate = isw.InstallationDate,
                    UninstallationDate = isw.UninstallationDate,
                    InstallationPath = isw.InstallationPath,
                    Notes = isw.Notes
                })
                .FirstOrDefault();
        }

        //Получить ПО, установленное на оборудовании
        public List<InstalledSoftwareDTO> GetByEquipmentId(int equipmentId)
        {
            try
            {
                return _context.InstalledSoftware
                    .Include(isw => isw.Equipment)
                    .Include(isw => isw.SoftwareLicense)
                    .Where(isw => isw.EquipmentId == equipmentId)
                    .AsNoTracking()
                    .Select(isw => new InstalledSoftwareDTO
                    {
                        Id = isw.Id,
                        EquipmentId = isw.EquipmentId,
                        EquipmentName = isw.Equipment.Name,
                        EquipmentInventoryNumber = isw.Equipment.InventoryNumber,
                        SoftwareLicenseId = isw.SoftwareLicenseId,
                        SoftwareName = isw.SoftwareLicense.SoftwareName,
                        SoftwareVersion = isw.SoftwareLicense.Version,
                        InstallationDate = isw.InstallationDate,
                        UninstallationDate = isw.UninstallationDate,
                        InstallationPath = isw.InstallationPath,
                        Notes = isw.Notes
                    })
                    .OrderBy(isw => isw.SoftwareName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении ПО для оборудования ID={equipmentId}: {ex.Message}", ex);
            }
        }

        //Получить активные установки на оборудовании
        public List<InstalledSoftwareDTO> GetActiveByEquipmentId(int equipmentId)
        {
            return GetByEquipmentId(equipmentId)
                .Where(isw => isw.IsActive)
                .ToList();
        }

        //Получить оборудование, на котором установлено ПО
        public List<InstalledSoftwareDTO> GetBySoftwareLicenseId(int softwareLicenseId)
        {
            try
            {
                return _context.InstalledSoftware
                    .Include(isw => isw.Equipment)
                    .ThenInclude(e => e.Employee)
                    .Include(isw => isw.SoftwareLicense)
                    .Where(isw => isw.SoftwareLicenseId == softwareLicenseId)
                    .AsNoTracking()
                    .Select(isw => new InstalledSoftwareDTO
                    {
                        Id = isw.Id,
                        EquipmentId = isw.EquipmentId,
                        EquipmentName = isw.Equipment.Name,
                        EquipmentInventoryNumber = isw.Equipment.InventoryNumber,
                        SoftwareLicenseId = isw.SoftwareLicenseId,
                        SoftwareName = isw.SoftwareLicense.SoftwareName,
                        SoftwareVersion = isw.SoftwareLicense.Version,
                        InstallationDate = isw.InstallationDate,
                        UninstallationDate = isw.UninstallationDate,
                        InstallationPath = isw.InstallationPath,
                        Notes = isw.Notes
                    })
                    .OrderBy(isw => isw.EquipmentName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении оборудования с ПО ID={softwareLicenseId}: {ex.Message}", ex);
            }
        }

        //Установить ПО на оборудование
        public void InstallSoftware(InstalledSoftwareCreateDTO dto)
        {
            try
            {
                // Валидация
                if (dto.EquipmentId <= 0)
                    throw new ArgumentException("Не указано оборудование");

                if (dto.SoftwareLicenseId <= 0)
                    throw new ArgumentException("Не указано ПО");

                // Проверяем существование оборудования
                var equipment = _context.Equipments.Find(dto.EquipmentId);
                if (equipment == null)
                    throw new ArgumentException($"Оборудование с ID={dto.EquipmentId} не найдено");

                // Проверяем существование лицензии
                var software = _context.SoftwareLicenses.Find(dto.SoftwareLicenseId);
                if (software == null)
                    throw new ArgumentException($"Лицензия ПО с ID={dto.SoftwareLicenseId} не найдена");

                // Проверяем, не установлено ли уже это ПО на оборудовании (активная установка)
                var existingInstallation = _context.InstalledSoftware
                    .FirstOrDefault(isw => isw.EquipmentId == dto.EquipmentId &&
                                          isw.SoftwareLicenseId == dto.SoftwareLicenseId &&
                                          isw.UninstallationDate == null);

                if (existingInstallation != null)
                    throw new ArgumentException($"ПО '{software.SoftwareName}' уже установлено на этом оборудовании");

                // Проверяем доступность лицензий
                var installedCount = _context.InstalledSoftware
                    .Count(isw => isw.SoftwareLicenseId == dto.SoftwareLicenseId &&
                                 isw.UninstallationDate == null);

                if (installedCount >= software.LicenseCount)
                    throw new ArgumentException($"Недостаточно лицензий для '{software.SoftwareName}'. " +
                        $"Доступно: {software.LicenseCount - installedCount}");

                // Проверяем срок действия лицензии
                if (software.ExpiryDate.HasValue && software.ExpiryDate.Value < DateTime.Now)
                    throw new ArgumentException($"Срок действия лицензии для '{software.SoftwareName}' истек ({software.ExpiryDate.Value:dd.MM.yyyy})");

                var installedSoftware = new InstalledSoftware
                {
                    EquipmentId = dto.EquipmentId,
                    SoftwareLicenseId = dto.SoftwareLicenseId,
                    InstallationDate = dto.InstallationDate ?? DateTime.Now,
                    InstallationPath = dto.InstallationPath?.Trim() ?? "",
                    Notes = dto.Notes?.Trim() ?? ""
                };

                _context.InstalledSoftware.Add(installedSoftware);
                _context.SaveChanges();

                Console.WriteLine($"ПО '{software.SoftwareName}' установлено на оборудование '{equipment.Name}' (ID={equipment.Id})");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка сохранения в БД: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        //Удалить ПО с оборудования (установить дату удаления)
        public void UninstallSoftware(int id)
        {
            try
            {
                var installedSoftware = _context.InstalledSoftware.Find(id);
                if (installedSoftware == null)
                    throw new ArgumentException($"Запись об установленном ПО с ID={id} не найдена");

                // Проверяем, не удалено ли уже
                if (installedSoftware.UninstallationDate.HasValue)
                    throw new InvalidOperationException("Это ПО уже было удалено ранее");

                installedSoftware.UninstallationDate = DateTime.Now;

                _context.SaveChanges();

                Console.WriteLine($"ПО с ID={id} помечено как удаленное");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка удаления ПО: {ex.Message}", ex);
            }
        }

        //Полное удаление записи
        public void Delete(int id)
        {
            try
            {
                var installedSoftware = _context.InstalledSoftware.Find(id);
                if (installedSoftware == null) return;

                _context.InstalledSoftware.Remove(installedSoftware);
                _context.SaveChanges();

                Console.WriteLine($"Запись об установленном ПО с ID={id} полностью удалена");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка удаления записи: {ex.Message}", ex);
            }
        }

        //Обновить информацию об установке
        public void Update(InstalledSoftwareDTO dto)
        {
            try
            {
                var installedSoftware = _context.InstalledSoftware.Find(dto.Id);
                if (installedSoftware == null)
                    throw new ArgumentException($"Запись об установленном ПО с ID={dto.Id} не найдена");

                installedSoftware.InstallationPath = dto.InstallationPath?.Trim() ?? "";
                installedSoftware.Notes = dto.Notes?.Trim() ?? "";

                if (dto.UninstallationDate.HasValue)
                {
                    installedSoftware.UninstallationDate = dto.UninstallationDate;
                }

                _context.SaveChanges();

                Console.WriteLine($"Запись об установленном ПО с ID={dto.Id} обновлена");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка обновления записи: {ex.Message}", ex);
            }
        }

        //Проверить, установлено ли ПО на оборудовании
        public bool IsSoftwareInstalled(int equipmentId, int softwareLicenseId)
        {
            return _context.InstalledSoftware
                .Any(isw => isw.EquipmentId == equipmentId &&
                           isw.SoftwareLicenseId == softwareLicenseId &&
                           isw.UninstallationDate == null);
        }

        //Получить статистику по установкам
        public Dictionary<string, object> GetStatistics()
        {
            var stats = new Dictionary<string, object>();

            try
            {
                stats["Всего установок"] = _context.InstalledSoftware.Count();

                stats["Активных установок"] = _context.InstalledSoftware
                    .Count(isw => isw.UninstallationDate == null);

                stats["Удаленных установок"] = _context.InstalledSoftware
                    .Count(isw => isw.UninstallationDate != null);

                stats["Оборудования с ПО"] = _context.InstalledSoftware
                    .Where(isw => isw.UninstallationDate == null)
                    .Select(isw => isw.EquipmentId)
                    .Distinct()
                    .Count();

                // Самые популярные программы
                var popularSoftware = _context.InstalledSoftware
                    .Include(isw => isw.SoftwareLicense)
                    .Where(isw => isw.UninstallationDate == null)
                    .GroupBy(isw => new { isw.SoftwareLicenseId, isw.SoftwareLicense.SoftwareName })
                    .Select(g => new
                    {
                        SoftwareName = g.Key.SoftwareName,
                        InstallCount = g.Count()
                    })
                    .OrderByDescending(x => x.InstallCount)
                    .Take(5)
                    .ToList();

                stats["Популярное ПО"] = popularSoftware;

                // Установки за последний месяц
                var lastMonth = DateTime.Now.AddMonths(-1);
                stats["Установок за месяц"] = _context.InstalledSoftware
                    .Count(isw => isw.InstallationDate >= lastMonth);

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка получения статистики: {ex.Message}", ex);
            }
        }

        //Получить доступное ПО для установки на конкретное оборудование
        public List<SoftwareLicenseDTO> GetAvailableSoftwareForEquipment(int equipmentId)
        {
            try
            {
                var installedSoftwareIds = _context.InstalledSoftware
                    .Where(isw => isw.EquipmentId == equipmentId && isw.UninstallationDate == null)
                    .Select(isw => isw.SoftwareLicenseId)
                    .ToList();

                var availableSoftware = _context.SoftwareLicenses
                    .Where(sl => !installedSoftwareIds.Contains(sl.Id) &&
                               (sl.ExpiryDate == null || sl.ExpiryDate > DateTime.Now))
                    .Select(sl => new SoftwareLicenseDTO
                    {
                        Id = sl.Id,
                        SoftwareName = sl.SoftwareName,
                        Publisher = sl.Publisher,
                        Version = sl.Version,
                        LicenseCount = sl.LicenseCount,
                        ExpiryDate = sl.ExpiryDate,
                        LicenseType = sl.LicenseType
                    })
                    .ToList();

                // Рассчитываем доступное количество лицензий для каждого ПО
                foreach (var software in availableSoftware)
                {
                    var installedCount = _context.InstalledSoftware
                        .Count(isw => isw.SoftwareLicenseId == software.Id &&
                                     isw.UninstallationDate == null);

                    software.InstalledCount = installedCount;
                    software.AvailableCount = software.LicenseCount - installedCount;
                }

                return availableSoftware.Where(s => s.AvailableCount > 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка получения доступного ПО: {ex.Message}", ex);
            }
        }

        //Получить лицензионный отчет по ПО
        public List<LicenseUsageReportDTO> GetLicenseUsageReport()
        {
            try
            {
                return _context.SoftwareLicenses
                    .Include(sl => sl.InstalledSoftware)
                    .AsNoTracking()
                    .Select(sl => new LicenseUsageReportDTO
                    {
                        SoftwareName = sl.SoftwareName,
                        Publisher = sl.Publisher,
                        Version = sl.Version,
                        TotalLicenses = sl.LicenseCount,
                        UsedLicenses = sl.InstalledSoftware.Count(isw => isw.UninstallationDate == null),
                        ExpiryDate = sl.ExpiryDate,
                        IsExpired = sl.ExpiryDate.HasValue && sl.ExpiryDate.Value < DateTime.Now
                    })
                    .OrderBy(r => r.SoftwareName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка формирования отчета по лицензиям: {ex.Message}", ex);
            }
        }
    }

    // DTO для отчета по использованию лицензий
    public class LicenseUsageReportDTO
    {
        public string SoftwareName { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public int TotalLicenses { get; set; }
        public int UsedLicenses { get; set; }
        public int AvailableLicenses => TotalLicenses - UsedLicenses;
        public DateTime? ExpiryDate { get; set; }
        public bool IsExpired { get; set; }
    }
}