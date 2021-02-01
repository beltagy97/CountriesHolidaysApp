using Microsoft.EntityFrameworkCore.Migrations;

namespace CountriesAndHolidaysApp.Migrations
{
    public partial class AddForeignKeyExplicitly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Countries_CountryID",
                table: "Holidays");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Holidays",
                newName: "countryID");

            migrationBuilder.RenameIndex(
                name: "IX_Holidays_CountryID",
                table: "Holidays",
                newName: "IX_Holidays_countryID");

            migrationBuilder.AlterColumn<int>(
                name: "countryID",
                table: "Holidays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Countries_countryID",
                table: "Holidays",
                column: "countryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Countries_countryID",
                table: "Holidays");

            migrationBuilder.RenameColumn(
                name: "countryID",
                table: "Holidays",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Holidays_countryID",
                table: "Holidays",
                newName: "IX_Holidays_CountryID");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Holidays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Countries_CountryID",
                table: "Holidays",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
