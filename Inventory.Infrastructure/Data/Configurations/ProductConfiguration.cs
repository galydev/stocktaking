using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ProductId")
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.CreationDate)
                .HasColumnName("CreationDate")
                .HasColumnType("datetime");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sku)
                .HasColumnName("Sku")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            var converter = new ValueConverter<decimal, double>(
                v => (double)v,
                v => (decimal)v
            );

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .HasConversion(converter);

            builder.Property(e => e.MinimunStock)
                .HasColumnName("MinimunStock")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.MaximumStock)
                .HasColumnName("MaximumStock")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(e => e.Stock)
                .HasColumnName("Stock")
                .HasColumnType("integer")
                .IsRequired(false);
        }
    }
}