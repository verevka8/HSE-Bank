namespace HSE_Bank.infrastructure.DTO
{
    public class BankDataDto
    {
        public List<BankAccountDto> Accounts { get; set; } = new List<BankAccountDto>();

        public List<OperationDto> Operations { get; set; } = new List<OperationDto>();
    }
}