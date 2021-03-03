using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Infrastructure.Migrations
{
    public partial class AddForekeysProductWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movement_ProductId",
                table: "Movement",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_WarehouseId",
                table: "Movement",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "PK_Movements_Product",
                table: "Movement",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "PK_Movements_Warehouse",
                table: "Movement",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PK_Movements_Product",
                table: "Movement");

            migrationBuilder.DropForeignKey(
                name: "PK_Movements_Warehouse",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Movement_ProductId",
                table: "Movement");

            migrationBuilder.DropIndex(
                name: "IX_Movement_WarehouseId",
                table: "Movement");
        }
    }
}