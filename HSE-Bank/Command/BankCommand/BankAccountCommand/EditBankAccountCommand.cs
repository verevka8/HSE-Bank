using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankAccountCommand
{
    public class EditBankAccountCommand : IBankCommand
    {
        private readonly Guid _bankAccountId;
        private readonly string _name;
        private string? _backup;
        
        public EditBankAccountCommand(BankService service, Guid bankAccountId, string name) : base(service)
        {
            _bankAccountId = bankAccountId;
            _name = name;
        }

        public override void Execute()
        {
            Service.EditBankAccount(_bankAccountId, _name);
            _backup = _name;
        }

        public override void Undo()
        {
            if (_backup == null)
            {
                return;
            }

            Service.EditBankAccount(_bankAccountId, _backup);
            _backup = null;
        }
    }
}