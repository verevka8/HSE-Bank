using HSE_Bank.Domain;
using HSE_Bank.Domain.Models;
using HSE_Bank.infrastructure.DTO;

namespace HSE_Bank.Infrastructure.Import
{
    public abstract class BankDataImporter
    {
        private readonly IBankRepository _repository;

        protected BankDataImporter(IBankRepository repository)
        {
            _repository = repository;
        }

        public void Import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден", filePath);
            }

            string content = File.ReadAllText(filePath);
            BankDataDto data = Parse(content);
            LoadData(data);
        }

        protected abstract BankDataDto Parse(string content);

        private void LoadData(BankDataDto data)
        {
            _repository.Clear();

            Dictionary<Guid, BankAccount> accounts = new Dictionary<Guid, BankAccount>();

            foreach (BankAccountDto accountDto in data.Accounts)
            {
                BankAccount account = new BankAccount(accountDto.Id, accountDto.Name, accountDto.Balance);
                _repository.AddBankAccount(account);
                accounts[account.Id] = account;
            }

            foreach (OperationDto operationDto in data.Operations)
            {
                if (!accounts.TryGetValue(operationDto.BankAccountId, out BankAccount? account))
                {
                    throw new InvalidDataException(
                        $"Не удалось найти счет {operationDto.BankAccountId} для операции {operationDto.Id}");
                }

                Category category = new Category(operationDto.CategoryName, operationDto.CategoryType);
                Operation operation = new Operation(operationDto.Id, account, operationDto.Type, operationDto.Amount,
                    category, operationDto.Date, operationDto.Description);
                _repository.AddOperation(operation);
            }
        }
    }
}