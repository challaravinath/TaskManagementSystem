using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProductCatalog.Domain.Entities;


namespace ProductCatalog.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //table name
            builder.ToTable("Products");    

            //Primary Key
            builder.HasKey(p => p.Id);
            // Configure SKU as owned type
            builder.OwnsOne(p => p.Sku, skuBuilder =>
            {
                skuBuilder.Property(s => s.Value)
                    .HasColumnName("Sku")
                    .HasMaxLength(8)
                    .IsRequired();

                // Add unique index on SKU
                skuBuilder.HasIndex(s => s.Value).IsUnique();
            });
            // Configure Money as owned type
            builder.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Amount)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2)
                    .IsRequired();

                priceBuilder.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired()
                    .HasDefaultValue("USD");
            });
            // Other properties
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired();
        }
    }
}
