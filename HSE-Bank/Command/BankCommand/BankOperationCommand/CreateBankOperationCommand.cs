using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankOperationCommand
{
    public class CreateBankOperationCommand : IBankCommand
    {
        private readonly Guid _bankAccountId;
        private readonly TransferType _type;
        private readonly int _amount;
        private readonly int _categoryId;
        private readonly string? _description;

        public CreateBankOperationCommand(BankService service, Guid bankAccountId, TransferType type, int amount,
            int categoryId, string? description = null) : base(service)
        {
            _bankAccountId = bankAccountId;
            _type = type;
            _amount = amount;
            _categoryId = categoryId;
            _description = description;
        }

        public override void Execute()
        {
            Service.CreateOperation(_bankAccountId, _type, _amount, _categoryId, _description);
        }

        public override void Undo()
        {
        }
    }
}