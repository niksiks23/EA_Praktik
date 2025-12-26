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
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DepartmentDTO> GetAll()
        {
            return _context.Departments
                .Select(d => new DepartmentDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Head = d.Head
                })
                .ToList();
        }

        public DepartmentDTO GetById(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null) return null;

            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Head = department.Head
            };
        }

        public void Create(DepartmentCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название подразделения не может быть пустым");

            var department = new Department
            {
                Name = dto.Name.Trim(),
                Head = dto.Head?.Trim() ?? string.Empty,
                Phone = string.Empty,
                Email = string.Empty
            };

            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Update(DepartmentDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название подразделения не может быть пустым");

            var department = _context.Departments.Find(dto.Id);
            if (department == null)
                throw new ArgumentException("Подразделение не найдено");

            department.Name = dto.Name.Trim();
            department.Head = dto.Head?.Trim() ?? string.Empty;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var department = _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefault(d => d.Id == id);

            if (department == null) return;

            // Если есть сотрудники, нельзя удалить
            if (department.Employees.Any())
            {
                throw new InvalidOperationException(
                    "Нельзя удалить подразделение, в котором есть сотрудники. " +
                    "Сначала переместите или удалите сотрудников.");
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
        }
    }
}