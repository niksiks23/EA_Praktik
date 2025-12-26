using DAL.Entities;
using System;

namespace DAL.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> Departments { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Equipment> Equipments { get; }
        IRepository<EquipmentType> EquipmentTypes { get; }
        IRepository<EquipmentHistory> EquipmentHistories { get; }
        IRepository<SoftwareLicense> SoftwareLicenses { get; }
        IRepository<InstalledSoftware> InstalledSoftware { get; }

        int Complete();
    }
}