using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class SelectRegRequirementUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedRegulationRequirement",
                schema: "ProcedureView",
                columns: table => new
                {
                    RegID = table.Column<int>(nullable: false),
                    CommentTypeID = table.Column<int>(nullable: false),
                    CommentID = table.Column<int>(nullable: false),
                    Requirement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedRegulationRequirement",
                schema: "ProcedureView");
        }
    }
}
