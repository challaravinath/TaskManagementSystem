using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Guid> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> ExistsAsync(string skuValue);
    }
}
