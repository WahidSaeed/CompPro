using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterAddTableComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RegulationDetail_RegDetailID",
                schema: "Library",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Regulation_RegID",
                schema: "Library",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "RegID",
                schema: "Library",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RegDetailID",
                schema: "Library",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "Library",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RegulationDetail_RegDetailID",
                schema: "Library",
                table: "Comments",
                column: "RegDetailID",
                principalSchema: "Library",
                principalTable: "RegulationDetail",
                principalColumn: "RegDetailId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Regulation_RegID",
                schema: "Library",
                table: "Comments",
                column: "RegID",
                principalSchema: "Library",
                principalTable: "Regulation",
                principalColumn: "RegId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RegulationDetail_RegDetailID",
                schema: "Library",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Regulation_RegID",
                schema: "Library",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "RegID",
                schema: "Library",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RegDetailID",
                schema: "Library",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                schema: "Library",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RegulationDetail_RegDetailID",
                schema: "Library",
                table: "Comments",
                column: "RegDetailID",
                principalSchema: "Library",
                principalTable: "RegulationDetail",
                principalColumn: "RegDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Regulation_RegID",
                schema: "Library",
                table: "Comments",
                column: "RegID",
                principalSchema: "Library",
                principalTable: "Regulation",
                principalColumn: "RegId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
