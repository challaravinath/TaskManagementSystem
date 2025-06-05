using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Products.Queries;

public record ProductDto(
    Guid Id,
    string Sku,
    string Name,
    decimal Price,
    bool IsActive);

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();

        // Map Domain entities to DTOs
        return products.Select(p => new ProductDto(
            p.Id,
            p.Sku.ToString(),  // Convert Sku object to string
            p.Name,
            p.Price.Amount,    // Get decimal from Money object
            p.IsActive
        ));
    }
}

