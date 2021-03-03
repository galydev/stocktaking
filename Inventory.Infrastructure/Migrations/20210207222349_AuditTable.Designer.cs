﻿// <auto-generated />
using System;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Infrastructure.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20210207222349_AuditTable")]
    partial class AuditTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Inventory.Domain.Entities.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AuditId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnName("Action")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("AuditDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AuditDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("KeyValues")
                        .IsRequired()
                        .HasColumnName("KeyValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewValues")
                        .IsRequired()
                        .HasColumnName("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .IsRequired()
                        .HasColumnName("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnName("TableName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnName("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MovementId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("MovementDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MovementDate")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("ProductId")
                        .HasColumnName("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnName("Quantity")
                        .HasColumnType("integer");

                    b.Property<bool>("Type")
                        .HasColumnName("Type")
                        .HasColumnType("bit")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<Guid>("WarehouseId")
                        .HasColumnName("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Movement");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("MaximumStock")
                        .HasColumnName("MaximumStock")
                        .HasColumnType("integer");

                    b.Property<int>("MinimunStock")
                        .HasColumnName("MinimunStock")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnName("Sku")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int?>("Stock")
                        .HasColumnName("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WarehouseId")
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnName("Location")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("MaximumCapacity")
                        .HasColumnName("MaximumCapacity")
                        .HasColumnType("integer");

                    b.Property<int>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Movement", b =>
                {
                    b.HasOne("Inventory.Domain.Entities.Product", "Product")
                        .WithMany("Movements")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("PK_Movements_Product")
                        .IsRequired();

                    b.HasOne("Inventory.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("Movements")
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("PK_Movements_Warehouse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
