namespace HSE_Bank.Domain.Models
{
    public class Category
    {
        public Guid Id { get; } = Guid.NewGuid();
        
        public TransferType Type { get; }
        
        public string Name { get; }


        public Category(string name, TransferType type)
        {
            Name = name;
            Type = type;
        }
    }
}