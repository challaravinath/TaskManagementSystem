using Microsoft.EntityFrameworkCore;

using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Infrastructure.Data;
using ProductCatalog.Infrastructure.Data.Configurations;
using ProductCatalog.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Add MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ProductCatalog.Application.AssemblyReference).Assembly));

// Add DbContext (using InMemory for now)
builder.Services.AddDbContext<ProductCatalogDbContext>(options =>
    options.UseInMemoryDatabase("ProductCatalog"));

// Add repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();