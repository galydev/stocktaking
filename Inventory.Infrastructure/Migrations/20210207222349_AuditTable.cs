using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Inventory.Infrastructure.Migrations
{
    public partial class AuditTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    AuditId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(maxLength: 200, nullable: false),
                    Action = table.Column<string>(maxLength: 200, nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.AuditId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");
        }
    }
}