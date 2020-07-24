using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterTableSelectedRegulationRequirementremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentTypeID",
                schema: "ProcedureView",
                table: "SelectedRegulationRequirement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentTypeID",
                schema: "ProcedureView",
                table: "SelectedRegulationRequirement",
                type: "int",
                nullable: true);
        }
    }
}
