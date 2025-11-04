namespace HSE_Bank.Command
{
    public interface IInvoker
    {
        void Run(ICommand cmd);
        void Undo();
    }
}