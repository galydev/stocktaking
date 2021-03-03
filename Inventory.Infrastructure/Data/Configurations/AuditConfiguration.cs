using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("Audit");

            builder.HasKey(e => e.AuditId);

            builder.Property(e => e.AuditId)
                .HasColumnName("AuditId")
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.AuditDate)
                .HasColumnName("AuditDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.User)
                .HasColumnName("User")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.TableName)
                .HasColumnName("TableName")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Action)
                .HasColumnName("Action")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.KeyValues)
                .HasColumnName("KeyValues")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.OldValues)
                .HasColumnName("OldValues")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(e => e.NewValues)
                .HasColumnName("NewValues")
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}