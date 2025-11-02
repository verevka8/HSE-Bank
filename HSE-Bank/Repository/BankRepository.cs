using HSE_Bank.Domain;
using HSE_Bank.Domain.Models;

namespace HSE_Bank
{
    public class BankRepository : IBankRepository
    {
        private Dictionary<Guid, BankAccount> _accounts = new Dictionary<Guid, BankAccount>();
        private Dictionary<Guid, Operation> _operations = new Dictionary<Guid, Operation>();

        public void AddBankAccount(BankAccount account)
        {
            _accounts[account.Id] = account;
        }

        public BankAccount GetBankAccount(Guid id)
        {
            if (_accounts.TryGetValue(id, out BankAccount? bankAccount))
            {
                return bankAccount;
            }

            throw new KeyNotFoundException("Такой аккаунт не найден");
        }

        public void AddOperation(Operation operation)
        {
            _operations[operation.Id] = operation; //TODO: проверка на повторы?
        }

        public Operation GetOperation(Guid id)
        {
            if (_operations.TryGetValue(id, out Operation? operation))
            {
                return operation;
            }

            throw new KeyNotFoundException("Такая операция не найдена");
        }

        public List<Operation> GetAllUserOperations(Guid bankAccountId)
        {
            return _operations.Where(pair => pair.Key == bankAccountId).Select(pair => pair.Value).ToList();
        }
    }
}