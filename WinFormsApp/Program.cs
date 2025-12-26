using BLL.Services;
using DAL.Data;
using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Console.WriteLine("ЗАПУСК ПРИЛОЖЕНИЯ");

                //Создание бд
                InitializeDatabase();

                //Создание серверов 
                Console.WriteLine("Создание сервисов...");

                using (var dbContext = new AppDbContext())
                {
                    // Основные сервисы
                    var equipmentService = new EquipmentService(dbContext);
                    var historyService = new EquipmentHistoryService(dbContext);
                    var employeeService = new EmployeeService(dbContext, equipmentService);
                    var departmentService = new DepartmentService(dbContext);

                    // Сервисы для ПО
                    var licenseService = new SoftwareLicenseService(dbContext);
                    var installedService = new InstalledSoftwareService(dbContext);

                    Console.WriteLine("Сервисы успешно созданы");
                    Console.WriteLine("==========================");

                    //Запуск гф
                    var mainForm = new MainForm(
                        departmentService,
                        employeeService,
                        equipmentService,
                        historyService,
                        licenseService,
                        installedService);

                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Критическая ошибка при запуске приложения", ex);
            }
        }

        static void InitializeDatabase()
        {
            try
            {
                Console.WriteLine("Инициализация базы данных...");

                using (var context = new AppDbContext())
                {
                    // Создаем БД если ее нет
                    bool dbCreated = context.Database.EnsureCreated();

                    if (dbCreated)
                    {
                        Console.WriteLine("База данных успешно создана");
                    }
                    else
                    {
                        Console.WriteLine("База данных уже существует");
                    }

                    // Выводим информацию о данных
                    PrintDatabaseInfo(context);
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Ошибка инициализации базы данных", ex);
                throw;
            }
        }

        static void PrintDatabaseInfo(AppDbContext context)
        {
            try
            {
                Console.WriteLine("\n=== ИНФОРМАЦИЯ О БАЗЕ ДАННЫХ ===");
                Console.WriteLine($"Подразделений: {context.Departments.Count()}");
                Console.WriteLine($"Сотрудников: {context.Employees.Count()}");
                Console.WriteLine($"Оборудования: {context.Equipments.Count()}");
                Console.WriteLine($"Типов оборудования: {context.EquipmentTypes.Count()}");
                Console.WriteLine($"Записей истории: {context.EquipmentHistories.Count()}");
                Console.WriteLine($"Лицензий ПО: {context.SoftwareLicenses.Count()}");
                Console.WriteLine($"Установок ПО: {context.InstalledSoftware.Count()}");
                Console.WriteLine("=================================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения информации о БД: {ex.Message}");
            }
        }

        static void ShowErrorDialog(string title, Exception ex)
        {
            string errorMessage = $"{title}:\n\n" +
                                $"Сообщение: {ex.Message}\n\n" +
                                $"Тип ошибки: {ex.GetType().Name}\n\n" +
                                $"StackTrace:\n{ex.StackTrace}";

            if (ex.InnerException != null)
            {
                errorMessage += $"\n\nВнутреннее исключение: {ex.InnerException.Message}";
            }

            MessageBox.Show(errorMessage, "Критическая ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            Console.WriteLine($"ОШИБКА: {title}");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
        }
    }
}