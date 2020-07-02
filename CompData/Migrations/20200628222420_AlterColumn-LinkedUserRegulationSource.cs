using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterColumnLinkedUserRegulationSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                schema: "Library",
                table: "LinkedUserRegulationSource");

            migrationBuilder.AddColumn<int>(
                name: "IsDefault",
                schema: "Library",
                table: "LinkedUserRegulationSource",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Library",
                table: "LinkedUserRegulationSource");

            migrationBuilder.AddColumn<int>(
                name: "Default",
                schema: "Library",
                table: "LinkedUserRegulationSource",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
