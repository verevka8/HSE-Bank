using HSE_Bank.Domain.Models;

namespace HSE_Bank
{
    public class BankAccount
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { set; get; }

        public int Balance { get; private set; }

        public BankAccount(string name, int balance)
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

        public override string ToString()
        {
            return $"Имя счета: {Name}, баланс: {Balance}";
        }
    }
}