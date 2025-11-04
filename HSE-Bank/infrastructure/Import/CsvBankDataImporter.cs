using System.Globalization;
using System.Text;
using HSE_Bank.Domain;
using HSE_Bank.infrastructure.DTO;

namespace HSE_Bank.Infrastructure.Import
{
    
    /// <summary>
    /// Этот класс написан с использованием ИИ (проект все таки про кпо и паттерны, а не про то, как распарсить csv строку)
    /// </summary>
    public class CsvBankDataImporter : BankDataImporter
    {
        public CsvBankDataImporter(IBankRepository repository) : base(repository)
        {
        }

        protected override BankDataDto Parse(string content)
        {
            BankDataDto data = new BankDataDto();
            using StringReader reader = new StringReader(content);
            string? header = reader.ReadLine();
            if (header == null)
            {
                return data;
            }

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                List<string> values = ParseLine(line);
                if (values.Count != 11)
                {
                    throw new InvalidDataException("Некорректное количество столбцов");
                }

                string entryType = values[0];
                switch (entryType.ToLowerInvariant())
                {
                    case "account":
                        data.Accounts.Add(new BankAccountDto
                        {
                            Id = Guid.Parse(values[1]),
                            Name = values[2],
                            Balance = int.Parse(values[3], CultureInfo.InvariantCulture)
                        });
                        break;
                    case "operation":
                        data.Operations.Add(new OperationDto
                        {
                            Id = Guid.Parse(values[1]),
                            BankAccountId = Guid.Parse(values[4]),
                            Amount = int.Parse(values[5], CultureInfo.InvariantCulture),
                            Type = Enum.Parse<TransferType>(values[6], true),
                            Date = DateTime.Parse(values[7], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                            Description = string.IsNullOrEmpty(values[8]) ? null : values[8],
                            CategoryName = values[9],
                            CategoryType = Enum.Parse<TransferType>(values[10], true)
                        });
                        break;
                    default:
                        throw new InvalidDataException($"ошибка, неизвестный тип: {entryType}");
                }
            }

            return data;
        }

        private static List<string> ParseLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            StringBuilder current = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            current.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
                else
                {
                    if (c == ',')
                    {
                        fields.Add(current.ToString());
                        current.Clear();
                    }
                    else if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
            }

            fields.Add(current.ToString());
            return fields;
        }
    }
}