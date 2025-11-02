using HSE_Bank.Domain.Models;

namespace HSE_Bank.Domain
{
    public interface IBankRepository
    {
        public void AddBankAccount(BankAccount account);

        public BankAccount GetBankAccount(Guid id);

        public void AddOperation(Operation operation);

        public Operation GetOperation(Guid id);

        public List<Operation> GetAllUserOperations(Guid bankAccountId);
    }
}