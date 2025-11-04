using HSE_Bank.Domain.Models;
using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankOperationCommand
{
    public class GetBankOperationInfoCommand : IBankCommand
    {
        private readonly Guid _id;
        public GetBankOperationInfoCommand(BankService service, Guid id) : base(service)
        {
            _id = id;
        }

        public override void Execute()
        {
            Operation operation = Service.GetOperation(_id);
            Console.WriteLine(operation + "\n");
        }

        public override void Undo()
        {
        }
    }
}