using HSE_Bank.Command;
using HSE_Bank.Command.BankCommand;
using HSE_Bank.Command.BankCommand.BankAccountCommand;
using HSE_Bank.Command.BankCommand.BankOperationCommand;
using HSE_Bank.Domain.Models;
using HSE_Bank.Service;

namespace HSE_Bank.infrastructure.ConsoleUI
{
    public class ConsoleUi
    {
        private readonly IInvoker _invoker;
        private readonly BankService _service;
        private readonly List<BankAccount> _accounts = new List<BankAccount>();

        public ConsoleUi(IInvoker invoker, BankService service)
        {
            _invoker = invoker;
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("Выберите:");
            Console.WriteLine("1. Создать счет\n" +
                              "2. Изменить счет\n" +
                              "3. Удалить счет\n" +
                              "4. Вывести информацию о счете\n" +
                              "5. Создать операцию\n" +
                              "6. Отредактировать операцию\n" +
                              "7. Вывести информацию о операции\n" +
                              "8. Вывести информацию о категории\n" +
                              "9. Отменить последнюю операцию (если возможно)");
            int n = int.Parse(Console.ReadLine()!);
            string name;
            Guid id;
            int choose = 0, choose1 = 0;
            switch (n)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Введите имя счета:");
                    name = Console.ReadLine()!;
                    Console.WriteLine("Введите баланс счета:");
                    int balance = int.Parse(Console.ReadLine()!);
                    ICommand command = new CreateBankAccountCommand(_service, name, balance);
                    _invoker.Run(command);
                    break;
                case 2:
                    id = ChooseBankAccount();
                    Console.Clear();
                    Console.WriteLine("Введите новое счета:");
                    name = Console.ReadLine()!;
                    _invoker.Run(new EditBankAccountCommand(_service, id, name));
                    break;
                case 3:
                    id = ChooseBankAccount();
                    _invoker.Run(new DeleteBankAccountCommand(_service, id));
                    break;
                case 4:
                    id = ChooseBankAccount();
                    _invoker.Run(new GetBankAccountInfoCommand(_service, id));
                    break;
                case 5:
                    id = ChooseBankAccount();
                    Console.WriteLine("Введите 1, если тип операция поступление, иначе 0");
                    choose = int.Parse(Console.ReadLine()!);
                    TransferType type = choose == 1 ? TransferType.Income : TransferType.Expense;
                    Console.WriteLine("Введите сумму:");
                    choose = int.Parse(Console.ReadLine()!);
                    choose1 = ChooseCategory();
                    Console.WriteLine("Введите описание или оставьте поле пустым");
                    string d = Console.ReadLine()!;
                    _invoker.Run(new CreateBankOperationCommand(_service, id, type, choose, choose1,
                        string.IsNullOrEmpty(d) ? null : d));
                    break;
                case 6:
                    
                case 9:
                    _invoker.Undo();
                    break;
            }
        }

        public Guid ChooseBankAccount()
        {
            Console.Clear();
            Console.WriteLine("Выберете аккаунт:\n");
            for (int i = 0; i < _accounts.Count; ++i)
            {
                Console.WriteLine($"{i + 1}. Имя счета: {_accounts[i].Name}, баланс: {_accounts[i].Balance}");
            }

            int n = int.Parse(Console.ReadLine()!);
            return _accounts[n - 1].Id;
        }

        public int ChooseCategory()
        {
            Console.Clear();
            Console.WriteLine("Выберете категорию:" +
                              "1. Зарплата\n" +
                              "2. Пособие\n" +
                              "3. Кешбек\n" +
                              "4. Кафе/Рестораны\n" +
                              "5. Супермаркеты\n" +
                              "6. ФастФуд\n" +
                              "7. Развлечения\n" +
                              "8. Гаджеты\n");
            int n = int.Parse(Console.ReadLine()!);
            if (n >= 1 && n <= 3)
            {
                return 10 + n - 1;
            }

            return 20 + n - 1;
        }
    }
}