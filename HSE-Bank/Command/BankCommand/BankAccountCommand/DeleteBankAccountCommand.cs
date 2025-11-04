using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankAccountCommand
{
    public class DeleteBankAccountCommand : IBankCommand
    {
        private readonly Guid _id;

        public DeleteBankAccountCommand(BankService service, Guid id) : base(service)
        {
            _id = id;
        }

        public override void Execute()
        { 
            Service.DeleteBankAccount(_id);
        }

        public override void Undo()
        {
            
        }
    }
}