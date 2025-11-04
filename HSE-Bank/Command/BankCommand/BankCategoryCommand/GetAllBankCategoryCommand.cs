using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankCategoryCommand
{
    public class GetAllBankCategoryCommand : IBankCommand
    {
        public GetAllBankCategoryCommand(BankService service) : base(service)
        {
        }

        public override void Execute()
        {
            Service.
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}