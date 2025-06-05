namespace ProductCatalog.Domain;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency = "USD")
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount can't be Negitive");
            Amount = Math.Round(amount, 2);
            Currency = currency.ToUpperInvariant();
        }
    }
    public static Money Zero => new(0);
}

