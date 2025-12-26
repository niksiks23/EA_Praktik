using BLL.DTOs;
using BLL.Interfaces;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class EquipmentService : IObservableEquipmentService
    {
        private readonly AppDbContext _context;
        private readonly List<IEquipmentObserver> _observers = new List<IEquipmentObserver>();
        private EquipmentHistoryService _historyService;

        public EquipmentService(AppDbContext context)
        {
            _context = context;
            _historyService = new EquipmentHistoryService(_context);
        }

        //МЕТОДЫ НАБЛЮДАТЕЛЯ
        public void Subscribe(IEquipmentObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"Наблюдатель добавлен. Всего наблюдателей: {_observers.Count}");
            }
        }

        public void Unsubscribe(IEquipmentObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"Наблюдатель удален. Всего наблюдателей: {_observers.Count}");
            }
        }

        public void NotifyEquipmentChanged()
        {
            Console.WriteLine($"Уведомление наблюдателей об изменении оборудования. Наблюдателей: {_observers.Count}");
            foreach (var observer in _observers.ToList())
            {
                try
                {
                    observer.OnEquipmentChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка уведомления наблюдателя: {ex.Message}");
                }
            }
        }

        public void NotifyEmployeeChanged()
        {
            Console.WriteLine($"Уведомление наблюдателей об изменении сотрудников. Наблюдателей: {_observers.Count}");
            foreach (var observer in _observers.ToList())
            {
                try
                {
                    observer.OnEmployeeChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка уведомления наблюдателя: {ex.Message}");
                }
            }
        }

        public void NotifyEquipmentTypeChanged()
        {
            Console.WriteLine($"Уведомление наблюдателей об изменении типов оборудования. Наблюдателей: {_observers.Count}");
            foreach (var observer in _observers.ToList())
            {
                try
                {
                    observer.OnEquipmentTypeChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка уведомления наблюдателя: {ex.Message}");
                }
            }
        }

        //Основа
        public List<EquipmentDTO> GetAll()
        {
            try
            {
                return _context.Equipments
                    .Include(e => e.Employee)
                    .Include(e => e.EquipmentType)
                    .AsNoTracking()
                    .Select(e => new EquipmentDTO
                    {
                        Id = e.Id,
                        InventoryNumber = e.InventoryNumber,
                        Name = e.Name,
                        EquipmentTypeId = e.EquipmentTypeId,
                        EquipmentTypeName = e.EquipmentType.Name,
                        SerialNumber = e.SerialNumber,
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.Employee != null ? e.Employee.FullName : "Не закреплено",
                        RegistrationDate = e.RegistrationDate,
                        PurchaseDate = e.PurchaseDate,
                        PurchasePrice = e.PurchasePrice,
                        Status = e.Status,
                        Location = e.Location,
                        Specifications = e.Specifications
                    })
                    .OrderBy(e => e.InventoryNumber)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении оборудования: {ex.Message}", ex);
            }
        }

        public EquipmentDTO GetById(int id)
        {
            return _context.Equipments
                .Include(e => e.Employee)
                .Include(e => e.EquipmentType)
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    InventoryNumber = e.InventoryNumber,
                    Name = e.Name,
                    EquipmentTypeId = e.EquipmentTypeId,
                    EquipmentTypeName = e.EquipmentType.Name,
                    SerialNumber = e.SerialNumber,
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.Employee != null ? e.Employee.FullName : "Не закреплено",
                    RegistrationDate = e.RegistrationDate,
                    PurchaseDate = e.PurchaseDate,
                    PurchasePrice = e.PurchasePrice,
                    Status = e.Status,
                    Location = e.Location,
                    Specifications = e.Specifications
                })
                .FirstOrDefault();
        }

        public List<EquipmentDTO> GetByEmployeeId(int employeeId)
        {
            return _context.Equipments
                .Include(e => e.Employee)
                .Include(e => e.EquipmentType)
                .Where(e => e.EmployeeId == employeeId)
                .AsNoTracking()
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    InventoryNumber = e.InventoryNumber,
                    Name = e.Name,
                    EquipmentTypeId = e.EquipmentTypeId,
                    EquipmentTypeName = e.EquipmentType.Name,
                    SerialNumber = e.SerialNumber,
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.Employee != null ? e.Employee.FullName : "Не закреплено",
                    RegistrationDate = e.RegistrationDate,
                    PurchaseDate = e.PurchaseDate,
                    PurchasePrice = e.PurchasePrice,
                    Status = e.Status,
                    Location = e.Location,
                    Specifications = e.Specifications
                })
                .ToList();
        }

        public List<EquipmentDTO> GetByDepartmentId(int departmentId)
        {
            return _context.Equipments
                .Include(e => e.Employee)
                .ThenInclude(emp => emp.Department)
                .Include(e => e.EquipmentType)
                .Where(e => e.Employee != null && e.Employee.DepartmentId == departmentId)
                .AsNoTracking()
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    InventoryNumber = e.InventoryNumber,
                    Name = e.Name,
                    EquipmentTypeId = e.EquipmentTypeId,
                    EquipmentTypeName = e.EquipmentType.Name,
                    SerialNumber = e.SerialNumber,
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.Employee.FullName,
                    RegistrationDate = e.RegistrationDate,
                    PurchaseDate = e.PurchaseDate,
                    PurchasePrice = e.PurchasePrice,
                    Status = e.Status,
                    Location = e.Location,
                    Specifications = e.Specifications
                })
                .ToList();
        }

        public List<EquipmentDTO> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAll();

            searchText = searchText.ToLower();

            return _context.Equipments
                .Include(e => e.Employee)
                .Include(e => e.EquipmentType)
                .Where(e => e.InventoryNumber.ToLower().Contains(searchText) ||
                           e.Name.ToLower().Contains(searchText) ||
                           e.SerialNumber.ToLower().Contains(searchText) ||
                           (e.Employee != null && e.Employee.FullName.ToLower().Contains(searchText)))
                .AsNoTracking()
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    InventoryNumber = e.InventoryNumber,
                    Name = e.Name,
                    EquipmentTypeId = e.EquipmentTypeId,
                    EquipmentTypeName = e.EquipmentType.Name,
                    SerialNumber = e.SerialNumber,
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.Employee != null ? e.Employee.FullName : "Не закреплено",
                    RegistrationDate = e.RegistrationDate,
                    PurchaseDate = e.PurchaseDate,
                    PurchasePrice = e.PurchasePrice,
                    Status = e.Status,
                    Location = e.Location,
                    Specifications = e.Specifications
                })
                .ToList();
        }

        public void Create(EquipmentCreateDTO dto)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(dto.InventoryNumber))
                    throw new ArgumentException("Инвентарный номер не может быть пустым");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    throw new ArgumentException("Название оборудования не может быть пустым");

                // Проверка уникальности инвентарного номера
                if (_context.Equipments.Any(e => e.InventoryNumber == dto.InventoryNumber))
                    throw new ArgumentException("Оборудование с таким инвентарным номером уже существует");

                // Проверка типа оборудования
                if (!_context.EquipmentTypes.Any(et => et.Id == dto.EquipmentTypeId))
                    throw new ArgumentException("Указанный тип оборудования не существует");

                // Проверка сотрудника
                if (dto.EmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.EmployeeId.Value))
                    throw new ArgumentException("Указанный сотрудник не существует");

                var equipment = new Equipment
                {
                    InventoryNumber = dto.InventoryNumber.Trim(),
                    Name = dto.Name.Trim(),
                    EquipmentTypeId = dto.EquipmentTypeId,
                    SerialNumber = dto.SerialNumber?.Trim() ?? "",
                    EmployeeId = dto.EmployeeId,
                    RegistrationDate = dto.RegistrationDate,
                    PurchaseDate = dto.PurchaseDate,
                    PurchasePrice = dto.PurchasePrice,
                    Status = dto.Status ?? "В работе",
                    Location = dto.Location?.Trim() ?? "",
                    Specifications = dto.Specifications?.Trim() ?? ""
                };

                _context.Equipments.Add(equipment);
                _context.SaveChanges();

                // Создаем начальную запись в истории при создании оборудования
                if (dto.EmployeeId.HasValue)
                {
                    _historyService.CreateForEquipmentChange(
                        equipmentId: equipment.Id,
                        oldEmployeeId: null,
                        newEmployeeId: dto.EmployeeId,
                        reason: "Первоначальное закрепление оборудования"
                    );
                }

                // Уведомляем наблюдателей об изменении оборудования
                NotifyEquipmentChanged();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка сохранения в БД: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        public void Update(EquipmentDTO dto)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(dto.InventoryNumber))
                    throw new ArgumentException("Инвентарный номер не может быть пустым");

                if (string.IsNullOrWhiteSpace(dto.Name))
                    throw new ArgumentException("Название оборудования не может быть пустым");

                var equipment = _context.Equipments.Find(dto.Id);
                if (equipment == null)
                    throw new ArgumentException("Оборудование не найдено");

                // Проверка уникальности инвентарного номера
                if (_context.Equipments.Any(e => e.InventoryNumber == dto.InventoryNumber && e.Id != dto.Id))
                    throw new ArgumentException("Оборудование с таким инвентарным номером уже существует");

                // Проверка типа оборудования
                if (!_context.EquipmentTypes.Any(et => et.Id == dto.EquipmentTypeId))
                    throw new ArgumentException("Указанный тип оборудования не существует");

                // Проверка сотрудника
                if (dto.EmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.EmployeeId.Value))
                    throw new ArgumentException("Указанный сотрудник не существует");

                // Сохраняем старое значение сотрудника ДО изменения
                var oldEmployeeId = equipment.EmployeeId;

                // Обновляем поля
                equipment.InventoryNumber = dto.InventoryNumber.Trim();
                equipment.Name = dto.Name.Trim();
                equipment.EquipmentTypeId = dto.EquipmentTypeId;
                equipment.SerialNumber = dto.SerialNumber?.Trim() ?? "";
                equipment.EmployeeId = dto.EmployeeId;
                equipment.RegistrationDate = dto.RegistrationDate;
                equipment.PurchaseDate = dto.PurchaseDate;
                equipment.PurchasePrice = dto.PurchasePrice;
                equipment.Status = dto.Status ?? "В работе";
                equipment.Location = dto.Location?.Trim() ?? "";
                equipment.Specifications = dto.Specifications?.Trim() ?? "";

                _context.SaveChanges();

                // Если изменился сотрудник, создаем запись в истории
                if (oldEmployeeId != dto.EmployeeId)
                {
                    _historyService.CreateForEquipmentChange(
                        equipmentId: dto.Id,
                        oldEmployeeId: oldEmployeeId,
                        newEmployeeId: dto.EmployeeId,
                        reason: "Смена ответственного сотрудника"
                    );
                }

                // Уведомляем наблюдателей об изменении оборудования
                NotifyEquipmentChanged();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ошибка обновления в БД: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            var equipment = _context.Equipments
                .Include(e => e.EquipmentHistories)
                .Include(e => e.InstalledSoftware)
                .FirstOrDefault(e => e.Id == id);

            if (equipment == null) return;

            // Проверяем, нет ли связанных записей
            if (equipment.EquipmentHistories.Any())
                throw new InvalidOperationException("Нельзя удалить оборудование с историей перемещений");

            if (equipment.InstalledSoftware.Any())
                throw new InvalidOperationException("Нельзя удалить оборудование с установленным ПО");

            _context.Equipments.Remove(equipment);
            _context.SaveChanges();

            // Уведомляем наблюдателей об изменении оборудования
            NotifyEquipmentChanged();
        }

        //Методы для отчетов
        public List<EquipmentReportDTO> GetEquipmentByDepartmentsReport()
        {
            try
            {
                return _context.Equipments
                    .Include(e => e.Employee)
                    .ThenInclude(emp => emp.Department)
                    .Include(e => e.EquipmentType)
                    .Where(e => e.Employee != null)
                    .AsNoTracking()
                    .GroupBy(e => new
                    {
                        DepartmentId = e.Employee.Department.Id,
                        DepartmentName = e.Employee.Department.Name,
                        EquipmentTypeId = e.EquipmentType.Id,
                        EquipmentTypeName = e.EquipmentType.Name
                    })
                    .Select(g => new EquipmentReportDTO
                    {
                        DepartmentId = g.Key.DepartmentId,
                        DepartmentName = g.Key.DepartmentName,
                        EquipmentTypeId = g.Key.EquipmentTypeId,
                        EquipmentTypeName = g.Key.EquipmentTypeName,
                        EquipmentCount = g.Count(),
                        TotalPrice = g.Sum(e => e.PurchasePrice ?? 0)
                    })
                    .OrderBy(r => r.DepartmentName)
                    .ThenBy(r => r.EquipmentTypeName)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка формирования отчета: {ex.Message}", ex);
            }
        }
        public string GetSimpleEquipmentReport()
        {
            try
            {
                var report = new StringBuilder();
                report.AppendLine("ОТЧЕТ ПО ОБОРУДОВАНИЮ");
                report.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}");
                report.AppendLine();

                // 1. Общая статистика
                report.AppendLine("1. ОБЩАЯ СТАТИСТИКА:");
                report.AppendLine("-------------------");

                var stats = GetStatistics();
                report.AppendLine($"• Всего оборудования: {stats["Всего оборудования"]}");
                report.AppendLine($"• В работе: {stats["В работе"]}");
                report.AppendLine($"• За сотрудниками: {stats["За сотрудниками"]}");
                report.AppendLine($"• На складе: {stats["На складе"]}");
                report.AppendLine($"• В ремонте: {stats["В ремонте"]}");
                report.AppendLine($"• Списано: {stats["Списано"]}");
                report.AppendLine($"• Утеряно: {_context.Equipments.Count(e => e.Status == "Утерян")}");
                report.AppendLine();

                // 2. Стоимость оборудования
                report.AppendLine("2. ФИНАНСОВАЯ СТАТИСТИКА:");
                report.AppendLine("------------------------");

                decimal totalCost = _context.Equipments
                    .Where(e => e.PurchasePrice.HasValue)
                    .Sum(e => e.PurchasePrice.Value);

                decimal avgCost = _context.Equipments
                    .Where(e => e.PurchasePrice.HasValue)
                    .Average(e => e.PurchasePrice.Value);

                var maxCostEquipment = _context.Equipments
                    .Include(e => e.EquipmentType)
                    .Where(e => e.PurchasePrice.HasValue)
                    .OrderByDescending(e => e.PurchasePrice)
                    .FirstOrDefault();

                report.AppendLine($"• Общая стоимость: {totalCost:C}");
                report.AppendLine($"• Средняя стоимость: {avgCost:C}");

                if (maxCostEquipment != null)
                {
                    report.AppendLine($"• Самое дорогое оборудование:");
                    report.AppendLine($"  {maxCostEquipment.InventoryNumber} - {maxCostEquipment.Name}");
                    report.AppendLine($"  Тип: {maxCostEquipment.EquipmentType?.Name}");
                    report.AppendLine($"  Стоимость: {maxCostEquipment.PurchasePrice:C}");
                }
                report.AppendLine();

                // 3. Оборудование по подразделениям
                report.AppendLine("3. ОБОРУДОВАНИЕ ПО ПОДРАЗДЕЛЕНИЯМ:");
                report.AppendLine("---------------------------------");

                var deptReport = GetEquipmentByDepartmentsReport();
                if (deptReport.Any())
                {
                    foreach (var item in deptReport)
                    {
                        report.AppendLine($"• {item.DepartmentName}:");
                        report.AppendLine($"  Количество: {item.EquipmentCount} ед.");
                        report.AppendLine($"  Стоимость: {item.TotalPrice:C}");
                        report.AppendLine($"  Средняя стоимость: {(item.TotalPrice / item.EquipmentCount):C}");
                    }
                }
                else
                {
                    report.AppendLine("Нет данных");
                }
                report.AppendLine();

                // 4. Оборудование по статусам
                report.AppendLine("4. ОБОРУДОВАНИЕ ПО СТАТУСАМ:");
                report.AppendLine("----------------------------");

                var statusReport = GetEquipmentStatusReport();
                if (statusReport.Any())
                {
                    foreach (var item in statusReport)
                    {
                        report.AppendLine($"• {item.Status}: {item.EquipmentCount} ед. ({item.TotalPrice:C})");
                    }
                }
                report.AppendLine();

                // 5. Топ-5 сотрудников с оборудованием
                report.AppendLine("5. ТОП-5 СОТРУДНИКОВ С ОБОРУДОВАНИЕМ:");
                report.AppendLine("-------------------------------------");

                var topEmployees = _context.Equipments
                    .Include(e => e.Employee)
                    .Where(e => e.EmployeeId != null)
                    .GroupBy(e => new { e.EmployeeId, e.Employee.FullName })
                    .Select(g => new
                    {
                        EmployeeName = g.Key.FullName,
                        EquipmentCount = g.Count(),
                        TotalCost = g.Sum(e => e.PurchasePrice ?? 0)
                    })
                    .OrderByDescending(x => x.EquipmentCount)
                    .Take(5)
                    .ToList();

                if (topEmployees.Any())
                {
                    int place = 1;
                    foreach (var emp in topEmployees)
                    {
                        report.AppendLine($"{place}. {emp.EmployeeName}");
                        report.AppendLine($"   Оборудования: {emp.EquipmentCount} ед.");
                        report.AppendLine($"   Стоимость: {emp.TotalCost:C}");
                        place++;
                    }
                }
                else
                {
                    report.AppendLine("Нет данных о закрепленном оборудовании");
                }
                report.AppendLine();

                // 6. Последние добавленные позиции
                report.AppendLine("6. ПОСЛЕДНИЕ ДОБАВЛЕННЫЕ ПОЗИЦИИ:");
                report.AppendLine("--------------------------------");

                var recentEquipment = _context.Equipments
                    .Include(e => e.Employee)
                    .Include(e => e.EquipmentType)
                    .OrderByDescending(e => e.RegistrationDate)
                    .Take(5)
                    .ToList();

                if (recentEquipment.Any())
                {
                    foreach (var eq in recentEquipment)
                    {
                        var employeeName = eq.Employee != null ? eq.Employee.FullName : "Не закреплено";
                        var cost = eq.PurchasePrice.HasValue ? eq.PurchasePrice.Value.ToString("C") : "не указана";

                        report.AppendLine($"• {eq.InventoryNumber} - {eq.Name}");
                        report.AppendLine($"  Тип: {eq.EquipmentType?.Name}");
                        report.AppendLine($"  Ответственный: {employeeName}");
                        report.AppendLine($"  Дата учета: {eq.RegistrationDate:dd.MM.yyyy}");
                        report.AppendLine($"  Стоимость: {cost}");
                        report.AppendLine($"  Статус: {eq.Status}");
                    }
                }
                report.AppendLine();

                // 7. Типы оборудования (статистика)
                report.AppendLine("7. СТАТИСТИКА ПО ТИПАМ ОБОРУДОВАНИЯ:");
                report.AppendLine("-----------------------------------");

                var equipmentTypes = _context.EquipmentTypes
                    .Select(et => new
                    {
                        et.Name,
                        et.Category,
                        Count = _context.Equipments.Count(e => e.EquipmentTypeId == et.Id),
                        TotalCost = _context.Equipments
                            .Where(e => e.EquipmentTypeId == et.Id && e.PurchasePrice.HasValue)
                            .Sum(e => e.PurchasePrice.Value)
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                if (equipmentTypes.Any())
                {
                    foreach (var type in equipmentTypes)
                    {
                        if (type.Count > 0)
                        {
                            report.AppendLine($"• {type.Name} ({type.Category}):");
                            report.AppendLine($"  Количество: {type.Count} ед.");
                            report.AppendLine($"  Стоимость: {type.TotalCost:C}");
                        }
                    }
                }
                report.AppendLine();

                // 8. Оборудование без ответственных
                report.AppendLine("8. ОБОРУДОВАНИЕ БЕЗ ОТВЕТСТВЕННЫХ:");
                report.AppendLine("----------------------------------");

                var unassignedEquipment = _context.Equipments
                    .Include(e => e.EquipmentType)
                    .Where(e => e.EmployeeId == null)
                    .ToList();

                if (unassignedEquipment.Any())
                {
                    report.AppendLine($"Всего: {unassignedEquipment.Count} ед.");
                    foreach (var eq in unassignedEquipment.Take(3))
                    {
                        report.AppendLine($"• {eq.InventoryNumber} - {eq.Name} ({eq.EquipmentType?.Name})");
                    }

                    if (unassignedEquipment.Count > 3)
                    {
                        report.AppendLine($"... и еще {unassignedEquipment.Count - 3} ед.");
                    }
                }
                else
                {
                    report.AppendLine("Все оборудование закреплено за сотрудниками");
                }
                report.AppendLine();

                // 9. Предупреждения
                report.AppendLine("9. ПРЕДУПРЕЖДЕНИЯ И РЕКОМЕНДАЦИИ:");
                report.AppendLine("---------------------------------");

                // Проверка на устаревшее оборудование
                var oldEquipment = _context.Equipments
                    .Where(e => e.PurchaseDate.HasValue &&
                               e.PurchaseDate.Value < DateTime.Now.AddYears(-3))
                    .Count();

                if (oldEquipment > 0)
                {
                    report.AppendLine($"Устаревшее оборудование (старше 3 лет): {oldEquipment} ед.");
                    report.AppendLine("Рекомендация: рассмотреть списание или модернизацию");
                }

                // Оборудование в ремонте долгое время
                var longRepair = _context.Equipments
                    .Count(e => e.Status == "В ремонте");

                if (longRepair > 0)
                {
                    report.AppendLine($"Оборудование в ремонте: {longRepair} ед.");
                    report.AppendLine("Рекомендация: ускорить ремонт или списать");
                }

                // Дорогое оборудование без ответственных
                var expensiveUnassigned = _context.Equipments
                    .Where(e => e.EmployeeId == null &&
                               e.PurchasePrice.HasValue &&
                               e.PurchasePrice > 50000)
                    .Count();

                if (expensiveUnassigned > 0)
                {
                    report.AppendLine($"Дорогое оборудование без ответственных: {expensiveUnassigned} ед.");
                    report.AppendLine("Рекомендация: закрепить за сотрудниками");
                }

                report.AppendLine();
                report.AppendLine("=================================");
                report.AppendLine("Отчет сформирован автоматически");
                report.AppendLine("Система учета оборудования v1.0");

                return report.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка формирования отчета: {ex.Message}", ex);
            }
        }
        public List<EquipmentStatusReportDTO> GetEquipmentStatusReport()
        {
            try
            {
                return _context.Equipments
                    .AsNoTracking()
                    .GroupBy(e => e.Status)
                    .Select(g => new EquipmentStatusReportDTO
                    {
                        Status = g.Key,
                        EquipmentCount = g.Count(),
                        TotalPrice = g.Sum(e => e.PurchasePrice ?? 0)
                    })
                    .OrderByDescending(r => r.EquipmentCount)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка формирования отчета по статусам: {ex.Message}", ex);
            }
        }

        //Вспомогательные методы
        public List<string> GetStatuses()
        {
            return new List<string>
            {
                "В работе",
                "На списании",
                "В ремонте",
                "В резерве",
                "Списан",
                "Утерян"
            };
        }

        public Dictionary<string, int> GetStatistics()
        {
            var stats = new Dictionary<string, int>();

            stats["Всего оборудования"] = _context.Equipments.Count();
            stats["В работе"] = _context.Equipments.Count(e => e.Status == "В работе");
            stats["За сотрудниками"] = _context.Equipments.Count(e => e.EmployeeId != null);
            stats["На складе"] = _context.Equipments.Count(e => e.EmployeeId == null);
            stats["В ремонте"] = _context.Equipments.Count(e => e.Status == "В ремонте");
            stats["Списано"] = _context.Equipments.Count(e => e.Status == "Списан");

            return stats;
        }
    }

    //dto
    public class EquipmentReportDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int EquipmentTypeId { get; set; }
        public string EquipmentTypeName { get; set; }
        public int EquipmentCount { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class EquipmentStatusReportDTO
    {
        public string Status { get; set; }
        public int EquipmentCount { get; set; }
        public decimal TotalPrice { get; set; }
    }

}