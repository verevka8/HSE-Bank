using HSE_Bank.Command.BankCommand;
using HSE_Bank.Service;

namespace HSE_Bank.Command
{
    public class DecoratorWorkingTimeCommand : ICommand
    {

        private readonly ICommand _decoratedCommand;
        public DecoratorWorkingTimeCommand(ICommand command)
        {
            _decoratedCommand = command;
        }

        public void Execute()
        {
            DateTime startTime = DateTime.Now;
            _decoratedCommand.Execute();
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения: {(endTime - startTime).TotalMilliseconds} мс");
        }

        public  void Undo()
        {
            DateTime startTime = DateTime.Now;
            _decoratedCommand.Undo();
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Время отмены команды: {(endTime - startTime).TotalMilliseconds} мс");
        }
    }
}