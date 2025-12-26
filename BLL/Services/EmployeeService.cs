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
    public class EmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IObservableEquipmentService _equipmentObservable;

        // Обновленный конструктор с возможностью передачи наблюдателя
        public EmployeeService(AppDbContext context, IObservableEquipmentService equipmentObservable = null)
        {
            _context = context;
            _equipmentObservable = equipmentObservable;
        }

        public List<EmployeeDTO> GetAll()
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Department)
                    .AsNoTracking()
                    .Select(e => new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        Position = e.Position,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.Department != null ? e.Department.Name : "Не указано"
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении сотрудников: {ex.Message}", ex);
            }
        }

        public EmployeeDTO GetById(int id)
        {
            return _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Position = e.Position,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : "Не указано"
                })
                .FirstOrDefault();
        }

        public void Create(EmployeeCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("ФИО сотрудника не может быть пустым");

            if (string.IsNullOrWhiteSpace(dto.Position))
                throw new ArgumentException("Должность не может быть пустой");

            // Проверяем существование подразделения
            var department = _context.Departments.Find(dto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Указанное подразделение не существует");

            var employee = new Employee
            {
                FullName = dto.FullName.Trim(),
                Position = dto.Position.Trim(),
                DepartmentId = dto.DepartmentId,
                Phone = string.Empty,
                Email = string.Empty
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            // Уведомляем об изменении сотрудников (если есть наблюдатель)
            _equipmentObservable?.NotifyEmployeeChanged();
        }

        public void Update(EmployeeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("ФИО сотрудника не может быть пустым");

            if (string.IsNullOrWhiteSpace(dto.Position))
                throw new ArgumentException("Должность не может быть пустой");

            var employee = _context.Employees.Find(dto.Id);
            if (employee == null)
                throw new ArgumentException("Сотрудник не найден");

            // Проверяем существование подразделения
            var department = _context.Departments.Find(dto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Указанное подразделение не существует");

            employee.FullName = dto.FullName.Trim();
            employee.Position = dto.Position.Trim();
            employee.DepartmentId = dto.DepartmentId;

            _context.SaveChanges();

            // Уведомляем об изменении сотрудников
            _equipmentObservable?.NotifyEmployeeChanged();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Equipments)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null) return;

            // Если есть закрепленное оборудование, нельзя удалить
            if (employee.Equipments.Any())
            {
                throw new InvalidOperationException(
                    "Нельзя удалить сотрудника, за которым закреплено оборудование. " +
                    "Сначала освободите оборудование.");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            // Уведомляем об изменении сотрудников
            _equipmentObservable?.NotifyEmployeeChanged();
        }
    }
}