using HSE_Bank.Domain.Models;

namespace HSE_Bank.Domain.Factory
{
    public class BankFactory
    {
        private readonly string[][] _categoryName =
        [
            ["Зарплата", "Пособие", "Кешбэк"], //income, поступления
            ["Кафе/Рестораны", "Супермаркеты", "ФастФуд", "Развлечения", "Гаджеты"] // траты
        ];

        private readonly Dictionary<int, Category> _cache = new Dictionary<int, Category>();

        public BankAccount CreateBankAccount(string name, int balance)
        {
            return new BankAccount(name, balance);
        }

        public Category GetCategory(int id)
        {
            if (id / 10 != 2 && id / 10 != 1)
            {
                throw new ArgumentException("Нет категории с таким id");
            }

            for (int i = 0; i < _categoryName.Length; ++i)
            {
                if (id / 10 == i + 1 && (id % 10 < 0 || id % 10 > _categoryName[i].Length))
                {
                    throw new ArgumentException("Нет категории с таким id");
                }
            }

            if (_cache.TryGetValue(id, out Category? value))
            {
                return value;
            }

            Category category = new Category(_categoryName[(id / 10) - 1][id % 10],
                id / 10 == 1 ? TransferType.Income : TransferType.Expense);
            value = category;
            _cache[id] = category;

            return value;
        }

        public Operation CreateOperation(BankAccount account, TransferType type, int amount, Category category,
            string description)
        {
            return new Operation(account, type, amount, category, description);
        }
    }
}