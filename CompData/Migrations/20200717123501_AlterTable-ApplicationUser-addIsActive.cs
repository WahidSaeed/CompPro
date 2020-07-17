using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterTableApplicationUseraddIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Security",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Security",
                table: "ApplicationUser");

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
