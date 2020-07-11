using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddColumnRegDescriptionCleanRegulationDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegDescriptionClean",
                schema: "Library",
                table: "RegulationDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegDescriptionClean",
                schema: "Library",
                table: "RegulationDetail");
        }
    }
}
