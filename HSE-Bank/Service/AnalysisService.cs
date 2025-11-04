using HSE_Bank.Domain;
using HSE_Bank.Domain.Models;

namespace HSE_Bank.Service
{
    public class AnalysisService
    {
        private readonly IBankRepository _repository;

        public AnalysisService(IBankRepository repository)
        {
            _repository = repository;
        }

        public int CalculateDifferenceBetweenIncAndExs(Guid bankAccountId, DateTime startDate, DateTime endDate)
        {
            List<Operation> operations = _repository.GetAllUserOperations(bankAccountId);
            int result = 0;
            foreach (Operation operation in operations)
            {
                result += operation.Amount * (operation.Type == TransferType.Income ? 1 : -1);
            }

            return result;
        }

        
        public Dictionary<Category, List<Operation>> GetTotalsByCategory(Guid bankAccountId, TransferType type)
        {
            List<Operation> operations = _repository.GetAllUserOperations(bankAccountId);
            return operations.Where(o => o.Type == type).GroupBy(o => o.OperationCategory)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        
        public Dictionary<Category, List<Operation>> GetExpensesByCategory(Guid bankAccountId)
        {
            return GetTotalsByCategory(bankAccountId, TransferType.Expense);
        }
        
        public Dictionary<Category, List<Operation>> GetIncomesByCategory(Guid bankAccountId)
        {
            return GetTotalsByCategory(bankAccountId, TransferType.Income);
        }
    }
}