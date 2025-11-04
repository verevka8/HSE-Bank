using HSE_Bank.Service;

namespace HSE_Bank.Command.BankCommand.BankOperationCommand
{
    public class EditBankOperationCommand : IBankCommand
    {
        private readonly Guid _id;
        private readonly int _newCategoryId;
        private readonly string? _newDescription;

        private int? _backupCategoryId;
        private string? _backupDescription;

        public EditBankOperationCommand(BankService service, Guid id, int newCategoryId, string? newDescription = null)
            : base(service)
        {
            _id = id;
            _newCategoryId = newCategoryId;
            _newDescription = newDescription;
        }

        public override void Execute()
        {
            _backupCategoryId = _newCategoryId;
            _backupDescription = _newDescription;
            Service.EditOperation(_id, _newCategoryId, _newDescription);
        }

        public override void Undo()
        {
            if (_backupCategoryId == null)
            {
                return;
            }

            Service.EditOperation(_id, (int)_backupCategoryId, _backupDescription);
            _backupDescription = null;
            _backupCategoryId = null;
        }
    }
}