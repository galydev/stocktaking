using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Inventory.Infrastructure.Migrations
{
    public partial class TableMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    MovementId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    MovementDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    Type = table.Column<bool>(unicode: false, maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.MovementId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movement");
        }
    }
}