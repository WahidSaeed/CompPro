using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class addedversioncolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "Library",
                table: "TagMapVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "Library",
                table: "RegulationSectionVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                schema: "Library",
                table: "LinkedRelatedRegulationVersion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Library",
                table: "TagMapVersion");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Library",
                table: "RegulationSectionVersion");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Library",
                table: "LinkedRelatedRegulationVersion");
        }
    }
}
