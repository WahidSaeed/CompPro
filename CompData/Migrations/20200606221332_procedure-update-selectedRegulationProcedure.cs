using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class procedureupdateselectedRegulationProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegDescription",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SectionId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RegDescription",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
