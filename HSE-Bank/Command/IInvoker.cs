namespace HSE_Bank.Command.BankCommand
{
    public interface IInvoker
    {
        public void Run(ICommand cmd);
        public void Undo();
    }
}