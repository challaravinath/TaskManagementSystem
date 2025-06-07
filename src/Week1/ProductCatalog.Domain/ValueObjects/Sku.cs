namespace ProductCatalog.Domain.ValueObjects;

public record Sku
{
    public string Value { get; }  // Add this property

    public Sku(string value)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Z]{3}-[0-9]{4}$"))
            throw new ArgumentException("SKU must be in format XXX-0000");

        Value = value;  // Use the property
    }

    public override string ToString() => Value;
}