using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class alterSelectedRegulationProcedureSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Summary",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
