using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class procedureViewRegulationFilteredBySource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RegulationFilteredBySource",
                schema: "ProcedureView",
                columns: table => new
                {
                    RegId = table.Column<int>(nullable: false),
                    RegulationTitle = table.Column<string>(nullable: true),
                    SourceId = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegulationFilteredBySource",
                schema: "ProcedureView");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
