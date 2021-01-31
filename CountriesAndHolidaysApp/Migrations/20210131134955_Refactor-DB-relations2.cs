using Microsoft.EntityFrameworkCore.Migrations;

namespace CountriesAndHolidaysApp.Migrations
{
    public partial class RefactorDBrelations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountriesCountryID",
                table: "Holidays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CountriesCountryID",
                table: "Holidays",
                column: "CountriesCountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Countries_CountriesCountryID",
                table: "Holidays",
                column: "CountriesCountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Countries_CountriesCountryID",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_CountriesCountryID",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CountriesCountryID",
                table: "Holidays");
        }
    }
}
