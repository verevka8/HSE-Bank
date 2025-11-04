using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankAccountCommand
{
    public class CreateBankAccountCommand : IBankCommand
    {
        private readonly string _name;
        private readonly int _balance;
        private BankAccount? _bankAccount;
        
        public CreateBankAccountCommand(BankService service, string name, int balance) : base(service)
        {
            _name = name;
            _balance = balance;
        }
        
        public override void Execute()
        {
            _bankAccount = Service.CreateBankAccount(_name, _balance);
        }

        public override void Undo()
        {
            if (_bankAccount == null)
            {
                throw new ArgumentException("Нельзя отменить несуществующую команду!");
            }

            Service.DeleteBankAccount(_bankAccount.Id);
            _bankAccount = null;
        }
    }
}