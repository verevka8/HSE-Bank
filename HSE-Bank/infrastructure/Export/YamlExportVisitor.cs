using System.IO;
using HSE_Bank.Domain.Export;
using HSE_Bank.Domain.Models;
using HSE_Bank.infrastructure.DTO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSE_Bank.Infrastructure.Export
{
    public class YamlExportVisitor : IExportVisitor
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
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string yaml = serializer.Serialize(_data);
            File.WriteAllText(filePath, yaml);
        }
    }
}