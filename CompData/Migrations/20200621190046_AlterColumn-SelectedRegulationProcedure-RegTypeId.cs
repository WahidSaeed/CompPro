using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterColumnSelectedRegulationProcedureRegTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegTypeId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegTypeId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");
        }
    }
}
