namespace HSE_Bank.Domain
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}