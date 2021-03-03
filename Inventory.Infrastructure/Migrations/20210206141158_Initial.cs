using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Inventory.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Sku = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimunStock = table.Column<decimal>(type: "integer", nullable: false),
                    MaximumStock = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Location = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    MaximumCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");
        }
    }
}