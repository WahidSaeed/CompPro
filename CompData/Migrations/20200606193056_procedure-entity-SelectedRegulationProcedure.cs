using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class procedureentitySelectedRegulationProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedRegulationProcedure",
                schema: "ProcedureView",
                columns: table => new
                {
                    RegId = table.Column<int>(nullable: false),
                    RegTitle = table.Column<string>(nullable: true),
                    SourceId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    SectionId = table.Column<string>(nullable: true),
                    SectionTitle = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    RegDescription = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedRegulationProcedure",
                schema: "ProcedureView");
        }
    }
}
