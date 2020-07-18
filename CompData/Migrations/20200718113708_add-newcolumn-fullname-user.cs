using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class addnewcolumnfullnameuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "Security",
                table: "ApplicationUser",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "Security",
                table: "ApplicationUser");
        }
    }
}
