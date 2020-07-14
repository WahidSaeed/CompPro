using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterTableRegulationMetafields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CommentID",
                schema: "ProcedureView",
                table: "SelectedRegulationRequirement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                schema: "Library",
                table: "Regulation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTag",
                schema: "Library",
                table: "Regulation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDescription",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "MetaTag",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.AlterColumn<int>(
                name: "CommentID",
                schema: "ProcedureView",
                table: "SelectedRegulationRequirement",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
