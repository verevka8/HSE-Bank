namespace HSE_Bank.infrastructure.DTO
{
    public class BankAccountDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Balance { get; set; }
    }
}