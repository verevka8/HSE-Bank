namespace HSE_Bank.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}