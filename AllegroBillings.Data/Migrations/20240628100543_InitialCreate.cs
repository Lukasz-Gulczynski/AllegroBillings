using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllegroBillings.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<string>(type: "TEXT", nullable: false),
                    ErpOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    StoreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Type_Name = table.Column<string>(type: "TEXT", nullable: false),
                    OfferId = table.Column<string>(type: "TEXT", nullable: true),
                    Value_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Value_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Tax_Percentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tax_Annotation = table.Column<string>(type: "TEXT", nullable: false),
                    Balance_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Balance_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    OrderId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billings_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Billings_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Billings_OfferId",
                table: "Billings",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_OrderId",
                table: "Billings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTables_Id",
                table: "OrderTables",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "OrderTables");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
