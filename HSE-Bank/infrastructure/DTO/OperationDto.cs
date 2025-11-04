namespace HSE_Bank.infrastructure.DTO
{
    public class OperationDto
    {
        public Guid Id { get; set; }

        public Guid BankAccountId { get; set; }

        public int Amount { get; set; }

        public TransferType Type { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public TransferType CategoryType { get; set; }
    }
}