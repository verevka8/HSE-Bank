using HSE_Bank.Command;
using HSE_Bank.Command.BankCommand;
using HSE_Bank.Domain;
using HSE_Bank.Domain.Factory;
using HSE_Bank.Domain.Models;
using HSE_Bank.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Serialization;


namespace HSE_Bank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ServiceCollection services = new();
            _ = services.AddSingleton<BankFactory>();
            _ = services.AddSingleton<BankService>();
            _ = services.AddSingleton<BankRepository>();
            _ = services.AddSingleton<CommandInvoker>();
            _ = services.AddSingleton<DataTransferService>();
            
            _ = services.AddSingleton<IInvoker>(sp => sp.GetRequiredService<CommandInvoker>());
            _ = services.AddSingleton<IBankRepository>(sp => sp.GetRequiredService<BankRepository>());
            
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            RunShowcase(serviceProvider);
            serviceProvider.Dispose();
        }

        private static void RunShowcase(ServiceProvider serviceProvider)
        {
            Console.WriteLine("=== Демонстрация возможностей HSE Bank ===\n");

            BankService bankService = serviceProvider.GetRequiredService<BankService>();
            BankRepository repository = serviceProvider.GetRequiredService<BankRepository>();
            DataTransferService dataTransferService = serviceProvider.GetRequiredService<DataTransferService>();
            
            

            BankAccount familyAccount = bankService.CreateBankAccount("Семейный бюджет", 20000);
            Console.WriteLine($"Создан счёт: {familyAccount}");

            BankAccount travelAccount = bankService.CreateBankAccount("Отпуск", 5000);
            Console.WriteLine($"Создан счёт: {travelAccount}");

            familyAccount = bankService.EditBankAccount(familyAccount.Id, "Основной счёт семьи");
            Console.WriteLine($"Переименованный счёт: {familyAccount}");

            Operation salary = bankService.CreateOperation(familyAccount.Id, TransferType.Income, 85000, 10,
                "Начисление зарплаты");
            Console.WriteLine($"Добавлена операция: {salary}");

            Operation cashback = bankService.CreateOperation(familyAccount.Id, TransferType.Income, 1500, 12,
                "Возврат по карте");
            Console.WriteLine($"Добавлена операция: {cashback}");

            Operation groceries = bankService.CreateOperation(familyAccount.Id, TransferType.Expense, 12500, 21,
                "Покупки на неделю");
            Console.WriteLine($"Добавлена операция: {groceries}");

            Operation entertainment = bankService.CreateOperation(familyAccount.Id, TransferType.Expense, 4000, 24,
                "AirPods");
            Console.WriteLine($"Добавлена операция: {entertainment}");

            Operation editedEntertainment = bankService.EditOperation(entertainment.Id, 23,
                "Перенесено в категорию развлечений");
            Console.WriteLine($"Операция после редактирования: {editedEntertainment}");

            Operation savings = bankService.CreateOperation(travelAccount.Id, TransferType.Income, 20000, 11,
                "Пополнение копилки");
            Console.WriteLine($"Добавлена операция: {savings}");

            Console.WriteLine("\nТекущие счета в системе:");
            foreach (BankAccount account in bankService.GetAllBankAccounts())
            {
                Console.WriteLine($" - {account}");
            }

            Console.WriteLine($"\nВсе операции по счёту '{familyAccount.Name}':");
            foreach (Operation operation in bankService.GetAllUserOperations(familyAccount.Id))
            {
                Console.WriteLine($" - {operation}");
            }

            Console.WriteLine("\nВсе операции в банке:");
            foreach (Operation operation in bankService.GetAllOperations())
            {
                Console.WriteLine($" - {operation}");
            }

            BankAccount updatedFamilyAccount = bankService.GetBankAccount(familyAccount.Id);
            Console.WriteLine(
                $"\nБаланс основного счёта после всех операций: {updatedFamilyAccount.Balance} у.е.");

            bankService.DeleteBankAccount(travelAccount.Id);
            Console.WriteLine(
                $"Удалён счёт '{travelAccount.Name}'. Количество оставшихся счетов: {bankService.GetAllBankAccounts().Count}");
            
            string demoDirectory = Path.Combine("/Users/verevka/RiderProjects/HSE-Bank/", "HSEBankDemo");
            Directory.CreateDirectory(demoDirectory);
            Console.WriteLine($"\nФайлы экспорта сохраняются в: {demoDirectory}");

            string jsonFilePath = Path.Combine(demoDirectory, "bank-data.json");
            dataTransferService.ExportToJson(jsonFilePath);
            Console.WriteLine($"Экспортировано в JSON: {jsonFilePath}");

            string yamlFilePath = Path.Combine(demoDirectory, "bank-data.yaml");
            dataTransferService.ExportToYaml(yamlFilePath);
            Console.WriteLine($"Экспортировано в YAML: {yamlFilePath}");

            string csvFilePath = Path.Combine(demoDirectory, "bank-data.csv");
            dataTransferService.ExportToCsv(csvFilePath);
            Console.WriteLine($"Экспортировано в CSV: {csvFilePath}");

            repository.Clear();
            Console.WriteLine(
                $"\nРепозиторий очищен для демонстрации импорта. Счётов: {repository.GetAllBankAccounts().Count}, операций: {repository.GetAllOperations().Count}");

            dataTransferService.ImportFromJson(jsonFilePath);
            Console.WriteLine(
                $"После импорта из JSON — счётов: {repository.GetAllBankAccounts().Count}, операций: {repository.GetAllOperations().Count}");
            
            repository.Clear();
        }
    }
}