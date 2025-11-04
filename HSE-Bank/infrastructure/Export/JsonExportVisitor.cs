using System.IO;
using System.Text.Json;
using HSE_Bank.Domain.Export;
using HSE_Bank.Domain.Models;
using HSE_Bank.infrastructure.DTO;

namespace HSE_Bank.Infrastructure.Export
{
    public class JsonExportVisitor : IExportVisitor
    {
        private readonly BankDataDto _data = new BankDataDto();

        public void Visit(BankAccount account)
        {
            _data.Accounts.Add(new BankAccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance
            });
        }

        public void Visit(Operation operation)
        {
            _data.Operations.Add(new OperationDto
            {
                Id = operation.Id,
                BankAccountId = operation.BankAccount.Id,
                Amount = operation.Amount,
                Type = operation.Type,
                Date = operation.Date,
                Description = operation.Description,
                CategoryName = operation.OperationCategory.Name,
                CategoryType = operation.OperationCategory.Type
            });
        }

        public void Save(string filePath)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(_data, options);
            File.WriteAllText(filePath, json);
        }
    }
}