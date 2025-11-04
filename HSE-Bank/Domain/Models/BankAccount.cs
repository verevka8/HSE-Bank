using HSE_Bank.Domain.Export;

namespace HSE_Bank.Domain.Models
{
    public class BankAccount : IPrototype<BankAccount>
    {
        public Guid Id { get; private set; }
        public string Name { set; get; }

        public int Balance { get; private set; }

        public BankAccount(string name, int balance) : this(Guid.NewGuid(), name, balance)
        {
        }

        public BankAccount(Guid id, string name, int balance) 
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Укажите корректное имя");
            }

            Name = name;
            if (balance < 0)
            {
                throw new ArgumentException("Баланс не может быть отрицательным");
            }

            Id = id;
            Balance = balance;
        }

        public void AcceptOperation(Operation operation)
        {
            if (operation.Type == TransferType.Expense && Balance < operation.Amount)
            {
                throw new InvalidOperationException("Недостаточно средств");
            }

            Balance += (operation.Type == TransferType.Income ? 1 : -1) * operation.Amount;
        }

        public BankAccount Clone()
        {
            return new BankAccount(Id, Name, Balance);
        }

        public override string ToString()
        {
            return $"Имя счета: {Name}, баланс: {Balance}";
        }
        
        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}