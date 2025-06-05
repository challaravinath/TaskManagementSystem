using MediatR;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Application.Products.Commands;

public record CreateProductCommand(
string Name,
string Description,
string Sku,
decimal Price) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public async Task<Guid> Handle
        (CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var sku = new Sku(request.Sku);
        if (await _productRepository.ExistsAsync(request.Sku))
            throw new ArgumentException($"Product with SKU {request.Sku} already exists.");

        var product = Product.Create(sku, request.Name, new Money(request.Price));

        return await _productRepository.AddAsync(product);

    }
}
