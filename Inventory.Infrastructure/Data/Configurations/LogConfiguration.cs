using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Data.Configurations
{
    class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();           

            builder.Property(e => e.Message)
                .HasColumnName("Message")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.MessageTemplate)
                .HasColumnName("MessageTemplate")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Level)
                .HasColumnName("Level")
                .HasColumnType("nvarchar(128)");

            builder.Property(e => e.TimeStamp)
                .HasColumnName("TimeStamp")
                .HasColumnType("datetime");

            builder.Property(e => e.Exception)
                .HasColumnName("Exception")
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Properties)
                .HasColumnName("Properties")
                .HasColumnType("nvarchar(max)");
        }
    }
}
