using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankCategoryCommand
{
    public class GetBankCategoryInfoCommand : IBankCommand
    {
        private readonly int _categoryId;

        public GetBankCategoryInfoCommand(BankService service, int categoryId) : base(service)
        {
            _categoryId = categoryId;
        }

        public override void Execute()
        {
            Console.WriteLine(Service.GetCategory(_categoryId));
        }

        public override void Undo()
        {
        }
    }
}