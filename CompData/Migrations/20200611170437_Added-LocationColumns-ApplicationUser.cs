using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedLocationColumnsApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Security",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "Security",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                schema: "Security",
                table: "ApplicationUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Region",
                schema: "Security",
                table: "ApplicationUser");
        }
    }
}
