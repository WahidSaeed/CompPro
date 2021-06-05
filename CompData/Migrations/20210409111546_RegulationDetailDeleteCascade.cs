using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class RegulationDetailDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection",
                column: "RegulationId",
                principalSchema: "Library",
                principalTable: "Regulation",
                principalColumn: "RegId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection",
                column: "RegulationId",
                principalSchema: "Library",
                principalTable: "Regulation",
                principalColumn: "RegId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
