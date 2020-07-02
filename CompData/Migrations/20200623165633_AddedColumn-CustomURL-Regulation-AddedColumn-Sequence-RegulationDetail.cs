using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedColumnCustomURLRegulationAddedColumnSequenceRegulationDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail");

            migrationBuilder.AddColumn<int>(
                name: "DescSequence",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegDescId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                schema: "Library",
                table: "RegulationDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomURL",
                schema: "Library",
                table: "Regulation",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail",
                column: "SectionId",
                principalSchema: "Library",
                principalTable: "RegulationSection",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail");

            migrationBuilder.DropColumn(
                name: "DescSequence",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");

            migrationBuilder.DropColumn(
                name: "RegDescId",
                schema: "ProcedureView",
                table: "SelectedRegulationProcedure");

            migrationBuilder.DropColumn(
                name: "Sequence",
                schema: "Library",
                table: "RegulationDetail");

            migrationBuilder.DropColumn(
                name: "CustomURL",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationDetail_RegulationSection_SectionId",
                schema: "Library",
                table: "RegulationDetail",
                column: "SectionId",
                principalSchema: "Library",
                principalTable: "RegulationSection",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
