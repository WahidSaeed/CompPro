using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterProcedureMetadetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomURL",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTag",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomURL",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");

            migrationBuilder.DropColumn(
                name: "MetaTag",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Security",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
