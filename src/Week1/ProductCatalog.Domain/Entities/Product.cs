using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public Sku Sku { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Product() { } // EF Core

        public static Product Create(Sku sku, string name, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required");

            return new Product
            {
                Id = Guid.NewGuid(),
                Sku = sku,
                Name = name,
                Price = price,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
