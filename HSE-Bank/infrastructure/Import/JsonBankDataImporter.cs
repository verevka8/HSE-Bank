using System.Text.Json;
using HSE_Bank.Domain;
using HSE_Bank.infrastructure.DTO;

namespace HSE_Bank.Infrastructure.Import
{
    public class JsonBankDataImporter : BankDataImporter
    {
        public JsonBankDataImporter(IBankRepository repository) : base(repository)
        {
        }

        protected override BankDataDto Parse(string content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            BankDataDto? data = JsonSerializer.Deserialize<BankDataDto>(content, options);
            if (data == null)
            {
                throw new InvalidDataException("Не удалось преобразовать JSON");
            }

            return data;
        }
    }
}