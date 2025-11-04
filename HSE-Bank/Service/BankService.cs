using HSE_Bank.Domain;
using HSE_Bank.Domain.Factory;
using HSE_Bank.Domain.Models;

namespace HSE_Bank.Service
{
    public class BankService
    {
        private readonly IBankRepository _repository;
        private readonly BankFactory _factory;

        public BankService(IBankRepository repository, BankFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public BankAccount CreateBankAccount(string name, int balance)
        {
            BankAccount newAccount = _factory.CreateBankAccount(name, balance);
            _repository.AddBankAccount(newAccount);
            return newAccount.Clone();
        }

        public BankAccount EditBankAccount(Guid id, string name)
        {
            BankAccount account = _repository.GetBankAccount(id);
            account.Name = name;
            return account.Clone();
        }

        public void DeleteBankAccount(Guid id)
        {
            _repository.DeleteAccount(id);
        }

        public BankAccount GetBankAccount(Guid id)
        {
            return _repository.GetBankAccount(id).Clone();
        }

        public Operation CreateOperation(Guid bankAccountId, TransferType type, int amount, int categoryId,
            string? description = null)
        {
            Operation operation = new Operation(_repository.GetBankAccount(bankAccountId), type, amount,
                _factory.GetCategory(categoryId), description);
            _repository.AddOperation(operation);
            return operation.Clone();
        }

        public Operation GetOperation(Guid id)
        {
            return _repository.GetOperation(id).Clone();
        }

        public Operation EditOperation(Guid id, int newCategoryId, string? newDescription = null)
        {
            Operation operation = _repository.GetOperation(id);
            operation.OperationCategory = _factory.GetCategory(newCategoryId);
            operation.Description = newDescription;
            return operation.Clone();
        }

        public Category GetCategory(int id)
        {
            return _factory.GetCategory(id);
        }
    }
}