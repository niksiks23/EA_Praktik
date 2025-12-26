using BLL.DTOs;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class EquipmentHistoryService
    {
        private readonly AppDbContext _context;

        public EquipmentHistoryService(AppDbContext context)
        {
            _context = context;
        }

        public List<EquipmentHistoryDTO> GetAll()
        {
            try
            {
                return _context.EquipmentHistories
                    .Include(eh => eh.Equipment)
                    .Include(eh => eh.OldEmployee)
                    .Include(eh => eh.NewEmployee)
                    .AsNoTracking()
                    .OrderByDescending(eh => eh.ChangeDate)
                    .Select(eh => new EquipmentHistoryDTO
                    {
                        Id = eh.Id,
                        EquipmentId = eh.EquipmentId,
                        EquipmentName = eh.Equipment.Name,
                        EquipmentInventoryNumber = eh.Equipment.InventoryNumber,
                        ChangeDate = eh.ChangeDate,
                        OldEmployeeId = eh.OldEmployeeId,
                        OldEmployeeName = eh.OldEmployee != null ? eh.OldEmployee.FullName : "Не указан",
                        NewEmployeeId = eh.NewEmployeeId,
                        NewEmployeeName = eh.NewEmployee != null ? eh.NewEmployee.FullName : "Не указан",
                        Reason = eh.Reason,
                        Notes = eh.Notes
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении истории перемещений: {ex.Message}", ex);
            }
        }

        public List<EquipmentHistoryDTO> GetByEquipmentId(int equipmentId)
        {
            try
            {
                return _context.EquipmentHistories
                    .Include(eh => eh.Equipment)
                    .Include(eh => eh.OldEmployee)
                    .Include(eh => eh.NewEmployee)
                    .Where(eh => eh.EquipmentId == equipmentId)
                    .AsNoTracking()
                    .OrderByDescending(eh => eh.ChangeDate)
                    .Select(eh => new EquipmentHistoryDTO
                    {
                        Id = eh.Id,
                        EquipmentId = eh.EquipmentId,
                        EquipmentName = eh.Equipment.Name,
                        EquipmentInventoryNumber = eh.Equipment.InventoryNumber,
                        ChangeDate = eh.ChangeDate,
                        OldEmployeeId = eh.OldEmployeeId,
                        OldEmployeeName = eh.OldEmployee != null ? eh.OldEmployee.FullName : "Не указан",
                        NewEmployeeId = eh.NewEmployeeId,
                        NewEmployeeName = eh.NewEmployee != null ? eh.NewEmployee.FullName : "Не указан",
                        Reason = eh.Reason,
                        Notes = eh.Notes
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении истории оборудования: {ex.Message}", ex);
            }
        }

        public List<EquipmentHistoryDTO> GetByEmployeeId(int employeeId)
        {
            try
            {
                return _context.EquipmentHistories
                    .Include(eh => eh.Equipment)
                    .Include(eh => eh.OldEmployee)
                    .Include(eh => eh.NewEmployee)
                    .Where(eh => eh.OldEmployeeId == employeeId || eh.NewEmployeeId == employeeId)
                    .AsNoTracking()
                    .OrderByDescending(eh => eh.ChangeDate)
                    .Select(eh => new EquipmentHistoryDTO
                    {
                        Id = eh.Id,
                        EquipmentId = eh.EquipmentId,
                        EquipmentName = eh.Equipment.Name,
                        EquipmentInventoryNumber = eh.Equipment.InventoryNumber,
                        ChangeDate = eh.ChangeDate,
                        OldEmployeeId = eh.OldEmployeeId,
                        OldEmployeeName = eh.OldEmployee != null ? eh.OldEmployee.FullName : "Не указан",
                        NewEmployeeId = eh.NewEmployeeId,
                        NewEmployeeName = eh.NewEmployee != null ? eh.NewEmployee.FullName : "Не указан",
                        Reason = eh.Reason,
                        Notes = eh.Notes
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении истории сотрудника: {ex.Message}", ex);
            }
        }

        public EquipmentHistoryDTO GetById(int id)
        {
            return _context.EquipmentHistories
                .Include(eh => eh.Equipment)
                .Include(eh => eh.OldEmployee)
                .Include(eh => eh.NewEmployee)
                .Where(eh => eh.Id == id)
                .AsNoTracking()
                .Select(eh => new EquipmentHistoryDTO
                {
                    Id = eh.Id,
                    EquipmentId = eh.EquipmentId,
                    EquipmentName = eh.Equipment.Name,
                    EquipmentInventoryNumber = eh.Equipment.InventoryNumber,
                    ChangeDate = eh.ChangeDate,
                    OldEmployeeId = eh.OldEmployeeId,
                    OldEmployeeName = eh.OldEmployee != null ? eh.OldEmployee.FullName : "Не указан",
                    NewEmployeeId = eh.NewEmployeeId,
                    NewEmployeeName = eh.NewEmployee != null ? eh.NewEmployee.FullName : "Не указан",
                    Reason = eh.Reason,
                    Notes = eh.Notes
                })
                .FirstOrDefault();
        }

        public void Create(EquipmentHistoryCreateDTO dto)
        {
            try
            {
                // Проверяем существование оборудования
                if (!_context.Equipments.Any(e => e.Id == dto.EquipmentId))
                    throw new ArgumentException("Указанное оборудование не существует");

                // Проверяем старых и новых сотрудников (если указаны)
                if (dto.OldEmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.OldEmployeeId.Value))
                    throw new ArgumentException("Указанный старый сотрудник не существует");

                if (dto.NewEmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.NewEmployeeId.Value))
                    throw new ArgumentException("Указанный новый сотрудник не существует");

                var history = new EquipmentHistory
                {
                    EquipmentId = dto.EquipmentId,
                    ChangeDate = dto.ChangeDate,
                    OldEmployeeId = dto.OldEmployeeId,
                    NewEmployeeId = dto.NewEmployeeId,
                    Reason = dto.Reason?.Trim() ?? "",
                    Notes = dto.Notes?.Trim() ?? ""
                };

                _context.EquipmentHistories.Add(history);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка создания записи истории: {ex.Message}", ex);
            }
        }

        public void CreateForEquipmentChange(int equipmentId, int? oldEmployeeId, int? newEmployeeId, string reason = "Смена ответственного")
        {
            try
            {
                var equipment = _context.Equipments.Find(equipmentId);
                if (equipment == null)
                {
                    Console.WriteLine($"Оборудование ID={equipmentId} не найдено для создания истории");
                    return;
                }

                var history = new EquipmentHistory
                {
                    EquipmentId = equipmentId,
                    ChangeDate = DateTime.Now,
                    OldEmployeeId = oldEmployeeId,
                    NewEmployeeId = newEmployeeId,
                    Reason = reason,
                    Notes = "Автоматически создано системой"
                };

                _context.EquipmentHistories.Add(history);
                _context.SaveChanges();

                Console.WriteLine($"Создана запись истории для оборудования '{equipment.Name}' (ID={equipmentId}): " +
                    $"старый сотрудник={oldEmployeeId}, новый сотрудник={newEmployeeId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания записи истории: {ex.Message}");
            }
        }

        public void Update(EquipmentHistoryDTO dto)
        {
            try
            {
                var history = _context.EquipmentHistories.Find(dto.Id);
                if (history == null)
                    throw new ArgumentException("Запись истории не найдена");

                // Проверяем существование оборудования
                if (!_context.Equipments.Any(e => e.Id == dto.EquipmentId))
                    throw new ArgumentException("Указанное оборудование не существует");

                // Проверяем сотрудников (если указаны)
                if (dto.OldEmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.OldEmployeeId.Value))
                    throw new ArgumentException("Указанный старый сотрудник не существует");

                if (dto.NewEmployeeId.HasValue && !_context.Employees.Any(e => e.Id == dto.NewEmployeeId.Value))
                    throw new ArgumentException("Указанный новый сотрудник не существует");

                history.EquipmentId = dto.EquipmentId;
                history.ChangeDate = dto.ChangeDate;
                history.OldEmployeeId = dto.OldEmployeeId;
                history.NewEmployeeId = dto.NewEmployeeId;
                history.Reason = dto.Reason?.Trim() ?? "";
                history.Notes = dto.Notes?.Trim() ?? "";

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка обновления записи истории: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            var history = _context.EquipmentHistories.Find(id);
            if (history == null) return;

            _context.EquipmentHistories.Remove(history);
            _context.SaveChanges();
        }

        public void DeleteByEquipmentId(int equipmentId)
        {
            var histories = _context.EquipmentHistories
                .Where(eh => eh.EquipmentId == equipmentId)
                .ToList();

            if (histories.Any())
            {
                _context.EquipmentHistories.RemoveRange(histories);
                _context.SaveChanges();
            }
        }

        // Получение статистики по истории
        public Dictionary<string, int> GetStatistics()
        {
            var stats = new Dictionary<string, int>();

            stats["Всего записей"] = _context.EquipmentHistories.Count();
            stats["За последний месяц"] = _context.EquipmentHistories
                .Count(eh => eh.ChangeDate >= DateTime.Now.AddMonths(-1));
            stats["За последнюю неделю"] = _context.EquipmentHistories
                .Count(eh => eh.ChangeDate >= DateTime.Now.AddDays(-7));

            return stats;
        }
    }
}