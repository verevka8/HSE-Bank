using HSE_Bank.Domain;
using HSE_Bank.Domain.Export;
using HSE_Bank.Domain.Models;
using HSE_Bank.infrastructure.Export;
using HSE_Bank.Infrastructure.Export;
using HSE_Bank.Infrastructure.Import;

namespace HSE_Bank.Service
{
    public class DataTransferService
    {
        private readonly IBankRepository _repository;

        public DataTransferService(IBankRepository repository)
        {
            _repository = repository;
        }

        public void ExportData(string filePath, IExportVisitor visitor)
        {
            foreach (BankAccount account in _repository.GetAllBankAccounts())
            {
                account.Accept(visitor);
            }

            foreach (Operation operation in _repository.GetAllOperations())
            {
                operation.Accept(visitor);
            }

            visitor.Save(filePath);
        }

        public void ExportToJson(string filePath)
        {
            ExportData(filePath, new JsonExportVisitor());
        }

        public void ExportToYaml(string filePath)
        {
            ExportData(filePath, new YamlExportVisitor());
        }

        public void ExportToCsv(string filePath)
        {
            ExportData(filePath, new CsvExportVisitor());
        }

        public void ImportFromJson(string filePath)
        {
            new JsonBankDataImporter(_repository).Import(filePath);
        }

        public void ImportFromYaml(string filePath)
        {
            new YamlBankDataImporter(_repository).Import(filePath);
        }

        public void ImportFromCsv(string filePath)
        {
            new CsvBankDataImporter(_repository).Import(filePath);
        }
    }
}