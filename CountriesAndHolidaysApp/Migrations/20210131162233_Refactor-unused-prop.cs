using Microsoft.EntityFrameworkCore.Migrations;

namespace CountriesAndHolidaysApp.Migrations
{
    public partial class Refactorunusedprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Holidays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
