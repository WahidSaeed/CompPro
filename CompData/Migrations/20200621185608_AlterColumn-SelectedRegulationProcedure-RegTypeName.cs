using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterColumnSelectedRegulationProcedureRegTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegTypeName",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegTypeName",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");
        }
    }
}
