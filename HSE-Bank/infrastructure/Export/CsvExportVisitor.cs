using HSE_Bank.Domain.Export;
using HSE_Bank.Domain.Models;
using HSE_Bank.infrastructure.DTO;
using System.Text;

namespace HSE_Bank.infrastructure.Export
{
    public class CsvExportVisitor : IExportVisitor
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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("type,id,name,balance,bankAccountId,amount,transferType,date,description,categoryName,categoryType");

            foreach (BankAccountDto account in _data.Accounts)
            {
                builder.AppendLine(string.Join(',', new[]
                {
                    "Account",
                    account.Id.ToString(),
                    Escape(account.Name),
                    account.Balance.ToString(),
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty
                }));
            }

            foreach (OperationDto operation in _data.Operations)
            {
                builder.AppendLine(string.Join(',', new[]
                {
                    "Operation",
                    operation.Id.ToString(),
                    string.Empty,
                    string.Empty,
                    operation.BankAccountId.ToString(),
                    operation.Amount.ToString(),
                    operation.Type.ToString(),
                    operation.Date.ToString("o"),
                    Escape(operation.Description ?? string.Empty),
                    Escape(operation.CategoryName),
                    operation.CategoryType.ToString()
                }));
            }

            File.WriteAllText(filePath, builder.ToString());
        }

        private static string Escape(string value)
        {
            if (value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
            }

            if (value.Contains(',') || value.Contains('\n') || value.Contains('\r') || value.Contains('"'))
            {
                return $"\"{value}\"";
            }

            return value;
        }
    }
}