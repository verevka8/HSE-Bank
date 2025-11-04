using HSE_Bank.Command;
using HSE_Bank.Command.BankCommand;
using HSE_Bank.Domain;
using HSE_Bank.Domain.Factory;
using HSE_Bank.infrastructure.ConsoleUI;
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
            _ = services.AddSingleton<ConsoleUi>();
            
            _ = services.AddSingleton<IInvoker>(sp => sp.GetRequiredService<CommandInvoker>());
            _ = services.AddSingleton<IBankRepository>(sp => sp.GetRequiredService<BankRepository>());
            
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ConsoleUi consoleUi = serviceProvider.GetRequiredService<ConsoleUi>();
            consoleUi.Run();
    
            serviceProvider.Dispose();
        }
    }
}