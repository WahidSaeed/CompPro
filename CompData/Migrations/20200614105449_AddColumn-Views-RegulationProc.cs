using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddColumnViewsRegulationProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Views",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure");
        }
    }
}
