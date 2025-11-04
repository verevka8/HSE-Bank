using HSE_Bank.Domain.Export;
using System.Runtime.InteropServices.JavaScript;

namespace HSE_Bank.Domain.Models
{
    public class Operation : IPrototype<Operation>
    {
        public Guid Id { get; private set; }

        public BankAccount BankAccount { get; }

        public int Amount { get; }

        public TransferType Type { get; }

        public DateTime Date { get; }

        public string? Description { set; get; }

        public Category OperationCategory { set; get; }

        public Operation(BankAccount bankAccount, TransferType type, int amount, Category category,
            string? description = null) : this(Guid.NewGuid(), bankAccount, type, amount, category, DateTime.Now, description)
        {
        }

        public Operation(Guid id, BankAccount bankAccount, TransferType type, int amount, Category category, DateTime date,
            string? description = null)
        {
            BankAccount = bankAccount;
            Type = type;
            if (amount < 0)
            {
                throw new ArgumentException("Баланс не может быть отрицательным");
            }

            Amount = amount;
            OperationCategory = category;
            Date = date;
            Description = description;
            Id = id;
            bankAccount.AcceptOperation(this);
        }

        public Operation Clone()
        {
            return new Operation(Id, BankAccount, Type, Amount, OperationCategory, Date, Description);
        }

        public override string ToString()
        {
            return
                $"Имя счета: {BankAccount.Name}, сумма: {Amount},  дата: {Date},  тип: {(Type == TransferType.Expense ? "Трата" : "Поступление")}, категория: {OperationCategory.Name}"
                + (Description == null ? "" : $", описание: {Description}");
        }
        
        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}