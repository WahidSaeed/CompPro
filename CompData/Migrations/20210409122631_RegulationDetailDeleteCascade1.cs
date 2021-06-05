using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class RegulationDetailDeleteCascade1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail",
                column: "SectionId",
                principalSchema: "Library",
                principalTable: "RegulationSection",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RegulationSection_Regulation_RegulationId",
                schema: "Library",
                table: "RegulationSection");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail",
                column: "SectionId",
                principalSchema: "Library",
                principalTable: "RegulationSection",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Restrict);

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
    }
}
