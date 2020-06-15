using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedRelationUserCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                schema: "Security",
                table: "ApplicationUser",
                maxLength: 3,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CountryCode",
                schema: "Security",
                table: "ApplicationUser",
                column: "CountryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Country_CountryCode",
                schema: "Security",
                table: "ApplicationUser",
                column: "CountryCode",
                principalSchema: "Config",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Country_CountryCode",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_CountryCode",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "Security",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
