using System.IO;
using HSE_Bank.Domain;
using HSE_Bank.infrastructure.DTO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HSE_Bank.Infrastructure.Import
{
    public class YamlBankDataImporter : BankDataImporter
    {
        public YamlBankDataImporter(IBankRepository repository) : base(repository)
        {
        }

        protected override BankDataDto Parse(string content)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            BankDataDto? data = deserializer.Deserialize<BankDataDto>(content);
            if (data == null)
            {
                throw new InvalidDataException("Не удалось преобразовать YAML");
            }

            return data;
        }
    }
}