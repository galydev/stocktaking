using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Infrastructure.Data.Configurations
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable("Movement");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("MovementId")
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.MovementDate)
                .HasColumnName("MovementDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.Type)
                .HasColumnName("Type")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Quantity)
                .HasColumnName("Quantity")
                .HasColumnType("integer")
                .IsRequired();

            var converter = new ValueConverter<decimal, double>(
                v => (double)v,
                v => (decimal)v
            );

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .HasConversion(converter);

            builder.Property(e => e.ProductId)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(e => e.WarehouseId)
                .HasColumnName("WarehouseId")
                .IsRequired();

            builder.HasOne(d => d.Product)
                .WithMany(m => m.Movements)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Movements_Product");
            builder.HasOne(d => d.Warehouse)
                .WithMany(m => m.Movements)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Movements_Warehouse");
        }
    }
}