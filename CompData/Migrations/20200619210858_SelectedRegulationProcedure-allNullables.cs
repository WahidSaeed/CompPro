using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class SelectedRegulationProcedureallNullables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource");

            migrationBuilder.DropColumn(
                name: "TypeName",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource");

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
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
