namespace HSE_Bank.Domain.Models
{
    public class Operation
    {
        public Guid Id { get; } = Guid.NewGuid();

        public BankAccount BankAccount { get; }

        public int Amount { get; }

        public TransferType Type { get; }

        public DateTime Date { get; }

        public string? Description { set; get; }

        public Category OperationCategory { set; get; }

        public Operation(BankAccount bankAccount, TransferType type, int amount, Category category,
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
            Date = DateTime.Now;
            Description = description;
            bankAccount.AcceptOperation(this);
        }

        public override string ToString()
        {
            return
                $"Имя счета: {BankAccount.Name}, сумма: {Amount},  дата: {Date},  тип: {(Type == TransferType.Expense ? "Трата" : "Поступление")}, категория: {OperationCategory.Name}"
                + (Description == null ? "" : $", описание: {Description}");
        }
    }
}