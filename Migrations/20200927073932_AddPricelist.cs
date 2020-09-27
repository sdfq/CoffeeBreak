using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeBreak.Migrations
{
    public partial class AddPricelist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pricelists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NumberOfWeek = table.Column<int>(nullable: false),
                    StartAndEndWeek = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricelists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pricelists");
        }
    }
}
