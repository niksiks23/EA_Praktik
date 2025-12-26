using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        // Добавляем конструктор с параметрами
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistories { get; set; }
        public DbSet<SoftwareLicense> SoftwareLicenses { get; set; }
        public DbSet<InstalledSoftware> InstalledSoftware { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если контекст уже настроен (например, в тестах), не настраиваем повторно
            if (!optionsBuilder.IsConfigured)
            {
                // Используем абсолютный путь к файлу БД
                string dbPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "equipment.db");

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department - Установка значений по умолчанию
            modelBuilder.Entity<Department>()
                .Property(d => d.Phone)
                .HasDefaultValue("");

            modelBuilder.Entity<Department>()
                .Property(d => d.Email)
                .HasDefaultValue("");

            // Employee - Установка значений по умолчанию
            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .HasDefaultValue("");

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .HasDefaultValue("");

            // Equipment - Индекс по инвентарному номеру
            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.InventoryNumber)
                .IsUnique();

            // Equipment - Конвертация enum/string для Status
            modelBuilder.Entity<Equipment>()
                .Property(e => e.Status)
                .HasConversion<string>();

            // Equipment - Значения по умолчанию
            modelBuilder.Entity<Equipment>()
                .Property(e => e.Location)
                .HasDefaultValue("");

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Specifications)
                .HasDefaultValue("");

            // EquipmentType - Значения по умолчанию
            modelBuilder.Entity<EquipmentType>()
                .Property(et => et.Description)
                .HasDefaultValue("");

            // SoftwareLicense - Значения по умолчанию
            modelBuilder.Entity<SoftwareLicense>()
                .Property(sl => sl.Notes)
                .HasDefaultValue("");

            // InstalledSoftware - Значения по умолчанию
            modelBuilder.Entity<InstalledSoftware>()
                .Property(isw => isw.InstallationPath)
                .HasDefaultValue("");

            modelBuilder.Entity<InstalledSoftware>()
                .Property(isw => isw.Notes)
                .HasDefaultValue("");

            // EquipmentHistory - Значения по умолчанию
            modelBuilder.Entity<EquipmentHistory>()
                .Property(eh => eh.Reason)
                .HasDefaultValue("");

            modelBuilder.Entity<EquipmentHistory>()
                .Property(eh => eh.Notes)
                .HasDefaultValue("");

            // Внешние ключи и связи
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Equipments)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.EquipmentType)
                .WithMany(et => et.Equipments)
                .HasForeignKey(e => e.EquipmentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EquipmentHistory>()
                .HasOne(eh => eh.Equipment)
                .WithMany(e => e.EquipmentHistories)
                .HasForeignKey(eh => eh.EquipmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EquipmentHistory>()
                .HasOne(eh => eh.OldEmployee)
                .WithMany(e => e.EquipmentHistoriesAsOld)
                .HasForeignKey(eh => eh.OldEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EquipmentHistory>()
                .HasOne(eh => eh.NewEmployee)
                .WithMany(e => e.EquipmentHistoriesAsNew)
                .HasForeignKey(eh => eh.NewEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InstalledSoftware>()
                .HasKey(isw => isw.Id);

            modelBuilder.Entity<InstalledSoftware>()
                .HasIndex(isw => new { isw.EquipmentId, isw.SoftwareLicenseId })
                .IsUnique();

            // ==================== SEED ДАННЫЕ ====================

            // Seed данные для EquipmentType (10 записей)
            modelBuilder.Entity<EquipmentType>().HasData(
                new EquipmentType { Id = 1, Name = "Системный блок", Category = "Компьютерная техника", Description = "Основной блок компьютера" },
                new EquipmentType { Id = 2, Name = "Монитор", Category = "Компьютерная техника", Description = "Дисплей для компьютера" },
                new EquipmentType { Id = 3, Name = "Ноутбук", Category = "Компьютерная техника", Description = "Переносной компьютер" },
                new EquipmentType { Id = 4, Name = "Принтер", Category = "Оргтехника", Description = "Устройство печати" },
                new EquipmentType { Id = 5, Name = "МФУ", Category = "Оргтехника", Description = "Многофункциональное устройство" },
                new EquipmentType { Id = 6, Name = "Сканер", Category = "Оргтехника", Description = "Устройство для оцифровки документов" },
                new EquipmentType { Id = 7, Name = "Маршрутизатор", Category = "Сетевое оборудование", Description = "Сетевое устройство" },
                new EquipmentType { Id = 8, Name = "Коммутатор", Category = "Сетевое оборудование", Description = "Сетевой коммутатор" },
                new EquipmentType { Id = 9, Name = "ИБП", Category = "Оборудование питания", Description = "Источник бесперебойного питания" },
                new EquipmentType { Id = 10, Name = "Проектор", Category = "Презентационное оборудование", Description = "Проекционное оборудование" }
            );

            // Seed данные для Department (5 записей)
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Отдел разработки",
                    Head = "Иванов Иван Иванович",
                    Phone = "+7 (895) 111-22-33",
                    Email = "dev@company.com"
                },
                new Department
                {
                    Id = 2,
                    Name = "Отдел тестирования",
                    Head = "Петрова Светлана Сергеевна",
                    Phone = "+7 (895) 111-44-55",
                    Email = "qa@company.com"
                },
                new Department
                {
                    Id = 3,
                    Name = "Отдел продаж",
                    Head = "Сидоров Алексей Викторович",
                    Phone = "+7 (895) 111-66-77",
                    Email = "sales@company.com"
                },
                new Department
                {
                    Id = 4,
                    Name = "Бухгалтерия",
                    Head = "Кузнецова Ольга Дмитриевна",
                    Phone = "+7 (895) 111-88-99",
                    Email = "accounting@company.com"
                },
                new Department
                {
                    Id = 5,
                    Name = "Техническая поддержка",
                    Head = "Васильев Денис Петрович",
                    Phone = "+7 (895) 111-00-11",
                    Email = "support@company.com"
                }
            );

            // Seed данные для Employee (10 записей)
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FullName = "Иванов Петр Сергеевич",
                    Position = "Старший разработчик",
                    Phone = "+7 (495) 123-45-67",
                    Email = "ivanov@company.com",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    FullName = "Петров Александр Владимирович",
                    Position = "Младший разработчик",
                    Phone = "+7 (495) 234-56-78",
                    Email = "petrov@company.com",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 3,
                    FullName = "Сидорова Анна Владимировна",
                    Position = "Тестировщик",
                    Phone = "+7 (495) 345-67-89",
                    Email = "sidorova@company.com",
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 4,
                    FullName = "Козлова Елена Сергеевна",
                    Position = "Старший тестировщик",
                    Phone = "+7 (495) 456-78-90",
                    Email = "kozlova@company.com",
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 5,
                    FullName = "Смирнов Максим Олегович",
                    Position = "Менеджер по продажам",
                    Phone = "+7 (495) 567-89-01",
                    Email = "smirnov@company.com",
                    DepartmentId = 3
                },
                new Employee
                {
                    Id = 6,
                    FullName = "Волкова Ольга Дмитриевна",
                    Position = "Бухгалтер",
                    Phone = "+7 (495) 678-90-12",
                    Email = "volkova@company.com",
                    DepartmentId = 4
                },
                new Employee
                {
                    Id = 7,
                    FullName = "Николаев Дмитрий Игоревич",
                    Position = "Специалист техподдержки",
                    Phone = "+7 (495) 789-01-23",
                    Email = "nikolaev@company.com",
                    DepartmentId = 5
                },
                new Employee
                {
                    Id = 8,
                    FullName = "Федорова Мария Александровна",
                    Position = "Ведущий специалист",
                    Phone = "+7 (495) 890-12-34",
                    Email = "fedorova@company.com",
                    DepartmentId = 5
                },
                new Employee
                {
                    Id = 9,
                    FullName = "Алексеев Сергей Петрович",
                    Position = "Системный администратор",
                    Phone = "+7 (495) 901-23-45",
                    Email = "alekseev@company.com",
                    DepartmentId = 5
                },
                new Employee
                {
                    Id = 10,
                    FullName = "Павлова Ирина Викторовна",
                    Position = "Финансовый аналитик",
                    Phone = "+7 (495) 012-34-56",
                    Email = "pavlova@company.com",
                    DepartmentId = 4
                }
            );

            // Seed данные для Equipment (примеры оборудования)
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    InventoryNumber = "IT-001",
                    Name = "Рабочая станция разработчика",
                    EquipmentTypeId = 1,
                    SerialNumber = "SN123456",
                    EmployeeId = 1,
                    RegistrationDate = new DateTime(2023, 1, 15),
                    PurchaseDate = new DateTime(2023, 1, 10),
                    PurchasePrice = 45000.00m,
                    Status = "В работе",
                    Location = "Кабинет 301",
                    Specifications = "Intel Core i7, 16GB RAM, 512GB SSD"
                },
                new Equipment
                {
                    Id = 2,
                    InventoryNumber = "IT-002",
                    Name = "Монитор Dell UltraSharp",
                    EquipmentTypeId = 2,
                    SerialNumber = "SN789012",
                    EmployeeId = 1,
                    RegistrationDate = new DateTime(2023, 1, 15),
                    PurchaseDate = new DateTime(2023, 1, 10),
                    PurchasePrice = 25000.00m,
                    Status = "В работе",
                    Location = "Кабинет 301",
                    Specifications = "27 дюймов, 4K UHD, USB-C"
                },
                new Equipment
                {
                    Id = 3,
                    InventoryNumber = "IT-003",
                    Name = "Ноутбук Lenovo ThinkPad",
                    EquipmentTypeId = 3,
                    SerialNumber = "SN345678",
                    EmployeeId = 3,
                    RegistrationDate = new DateTime(2023, 2, 20),
                    PurchaseDate = new DateTime(2023, 2, 15),
                    PurchasePrice = 65000.00m,
                    Status = "В работе",
                    Location = "Кабинет 205",
                    Specifications = "Intel Core i5, 8GB RAM, 256GB SSD"
                },
                new Equipment
                {
                    Id = 4,
                    InventoryNumber = "IT-004",
                    Name = "Принтер HP LaserJet",
                    EquipmentTypeId = 4,
                    SerialNumber = "SN901234",
                    EmployeeId = null,
                    RegistrationDate = new DateTime(2023, 3, 10),
                    PurchaseDate = new DateTime(2023, 3, 5),
                    PurchasePrice = 18000.00m,
                    Status = "В работе",
                    Location = "Ресепшн",
                    Specifications = "Черно-белый лазерный, 20 стр/мин"
                },
                new Equipment
                {
                    Id = 5,
                    InventoryNumber = "IT-005",
                    Name = "Маршрутизатор Cisco",
                    EquipmentTypeId = 7,
                    SerialNumber = "SN567890",
                    EmployeeId = null,
                    RegistrationDate = new DateTime(2023, 4, 5),
                    PurchaseDate = new DateTime(2023, 4, 1),
                    PurchasePrice = 35000.00m,
                    Status = "В работе",
                    Location = "Серверная",
                    Specifications = "Гигабитный, 24 порта"
                }
            );

            // Seed данные для SoftwareLicense (примеры лицензий ПО)
            modelBuilder.Entity<SoftwareLicense>().HasData(
                new SoftwareLicense
                {
                    Id = 1,
                    SoftwareName = "Microsoft Office 365",
                    Publisher = "Microsoft",
                    Version = "2021",
                    LicenseKey = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX",
                    PurchaseDate = new DateTime(2023, 1, 1),
                    ExpiryDate = new DateTime(2024, 12, 31),
                    LicenseCount = 50,
                    LicenseType = "Корпоративная",
                    PurchasePrice = 150000.00m,
                    Notes = "Для всех сотрудников компании"
                },
                new SoftwareLicense
                {
                    Id = 2,
                    SoftwareName = "Visual Studio Professional",
                    Publisher = "Microsoft",
                    Version = "2022",
                    LicenseKey = "YYYYY-YYYYY-YYYYY-YYYYY-YYYYY",
                    PurchaseDate = new DateTime(2023, 2, 1),
                    ExpiryDate = new DateTime(2024, 12, 31),
                    LicenseCount = 10,
                    LicenseType = "Профессиональная",
                    PurchasePrice = 50000.00m,
                    Notes = "Для отдела разработки"
                },
                new SoftwareLicense
                {
                    Id = 3,
                    SoftwareName = "Adobe Photoshop",
                    Publisher = "Adobe",
                    Version = "2023",
                    LicenseKey = "ZZZZZ-ZZZZZ-ZZZZZ-ZZZZZ-ZZZZZ",
                    PurchaseDate = new DateTime(2023, 3, 1),
                    ExpiryDate = new DateTime(2024, 6, 30),
                    LicenseCount = 5,
                    LicenseType = "Коммерческая",
                    PurchasePrice = 25000.00m,
                    Notes = "Для дизайнеров"
                }
            );

            // Seed данные для InstalledSoftware (примеры установленного ПО)
            modelBuilder.Entity<InstalledSoftware>().HasData(
                new InstalledSoftware
                {
                    Id = 1,
                    EquipmentId = 1,
                    SoftwareLicenseId = 1,
                    InstallationDate = new DateTime(2023, 1, 16),
                    UninstallationDate = null,
                    InstallationPath = "C:\\Program Files\\Microsoft Office",
                    Notes = "Установлено по умолчанию"
                },
                new InstalledSoftware
                {
                    Id = 2,
                    EquipmentId = 1,
                    SoftwareLicenseId = 2,
                    InstallationDate = new DateTime(2023, 1, 17),
                    UninstallationDate = null,
                    InstallationPath = "C:\\Program Files\\Microsoft Visual Studio",
                    Notes = "Для разработки"
                },
                new InstalledSoftware
                {
                    Id = 3,
                    EquipmentId = 3,
                    SoftwareLicenseId = 1,
                    InstallationDate = new DateTime(2023, 2, 21),
                    UninstallationDate = null,
                    InstallationPath = "C:\\Program Files\\Microsoft Office",
                    Notes = "Стандартная установка"
                }
            );
        }
    }
}