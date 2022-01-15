using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace example.Migrations
{
    public partial class AddCountryRelationToLoginHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Logins_CountryId",
                table: "Logins",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Countries_CountryId",
                table: "Logins",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Countries_CountryId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CountryId",
                table: "Logins");
        }
    }
}
