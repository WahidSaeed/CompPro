using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class alterusermodelprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                schema: "Security",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                schema: "Security",
                table: "ApplicationUser",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteURL",
                schema: "Security",
                table: "ApplicationUser",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Designation",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "WebsiteURL",
                schema: "Security",
                table: "ApplicationUser");
        }
    }
}
