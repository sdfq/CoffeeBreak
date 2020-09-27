using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeBreak.Migrations
{
    public partial class AddCoffeeProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartAndEndWeek",
                table: "Pricelists",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CoffeeProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PriceBigPacket = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceSmallPacket = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceBigPacketDiscount10 = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceSmallPacketDiscount10 = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceBigPacketDiscount20 = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceSmallPacketDiscount20 = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceBigPacketDiscount30 = table.Column<decimal>(type: "decimal", nullable: false),
                    PriceSmallPacketDiscount30 = table.Column<decimal>(type: "decimal", nullable: false),
                    PricelistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeeProducts_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeProducts_PricelistId",
                table: "CoffeeProducts",
                column: "PricelistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeProducts");

            migrationBuilder.AlterColumn<string>(
                name: "StartAndEndWeek",
                table: "Pricelists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
