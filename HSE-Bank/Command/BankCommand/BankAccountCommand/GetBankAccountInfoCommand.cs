using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankAccountCommand
{
    public class GetBankAccountInfoCommand : IBankCommand
    {
        private readonly Guid _id;
        public GetBankAccountInfoCommand(BankService service, Guid id) : base(service)
        {
            _id = id;
        }

        public override void Execute()
        {
            BankAccount account = Service.GetBankAccount(_id);
            Console.WriteLine($"{account}\n");
        }

        public override void Undo()
        {
            
        }
    }
}