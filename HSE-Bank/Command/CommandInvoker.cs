using HSE_Bank.Command.BankCommand;

namespace HSE_Bank.Command
{
    public class CommandInvoker : IInvoker
    {
        private readonly Stack<ICommand> _history = new();

        public void Run(ICommand cmd)
        {
            cmd.Execute();
            _history.Push(cmd);
        }

        public void Undo()
        {
            if (_history.Count == 0)
            {
                return;
            }

            ICommand cmd = _history.Pop();
            cmd.Undo();
        }
    }
}