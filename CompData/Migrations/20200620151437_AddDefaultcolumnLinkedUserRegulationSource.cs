using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddDefaultcolumnLinkedUserRegulationSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Default",
                schema: "Library",
                table: "LinkedUserRegulationSource",
                nullable: false,
                defaultValue: 0);
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

            migrationBuilder.DropColumn(
                name: "Default",
                schema: "Library",
                table: "LinkedUserRegulationSource");
        }
    }
}
