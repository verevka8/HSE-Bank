using HSE_Bank.Domain.Models;

namespace HSE_Bank.Domain
{
    public interface IBankRepository
    {
        void AddBankAccount(BankAccount account);

        BankAccount GetBankAccount(Guid id);

        void DeleteAccount(Guid id);
        
        List<BankAccount> GetAllBankAccounts();

        void AddOperation(Operation operation);

        Operation GetOperation(Guid id);

        List<Operation> GetAllUserOperations(Guid bankAccountId);
        
        List<Operation> GetAllOperations();
        void Clear();
    }
}