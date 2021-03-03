using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations
{
    internal class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouse");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("WarehouseId")
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.CreationDate)
                .HasColumnName("CreationDate")
                .HasColumnType("datetime");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Location)
                .HasColumnName("Location")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.MaximumCapacity)
                .IsRequired()
                .HasColumnName("MaximumCapacity")
                .HasColumnType("integer");
        }
    }
}