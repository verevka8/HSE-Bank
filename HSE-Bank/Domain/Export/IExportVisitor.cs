using HSE_Bank.Domain.Models;

namespace HSE_Bank.Domain.Export
{
    public interface IExportVisitor
    {
        void Visit(BankAccount account);

        void Visit(Operation operation);

        void Save(string filePath);
    }
}