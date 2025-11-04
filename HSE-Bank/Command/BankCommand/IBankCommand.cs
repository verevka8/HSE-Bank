using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand
{
    public abstract class IBankCommand : ICommand
    {
        protected readonly BankService Service;
        protected IBankCommand(BankService service)
        {
            Service = service;
        }
        public abstract void Execute();
        public abstract void Undo();
    }
}