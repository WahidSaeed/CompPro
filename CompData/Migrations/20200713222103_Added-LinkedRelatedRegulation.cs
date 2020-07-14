using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedLinkedRelatedRegulation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedRelatedRegulation",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegId = table.Column<int>(nullable: false),
                    RelatedRegId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedRelatedRegulation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedRelatedRegulation_Regulation_RegId",
                        column: x => x.RegId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkedRelatedRegulation_Regulation_RelatedRegId",
                        column: x => x.RelatedRegId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkedRelatedRegulation_RegId",
                schema: "Library",
                table: "LinkedRelatedRegulation",
                column: "RegId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedRelatedRegulation_RelatedRegId",
                schema: "Library",
                table: "LinkedRelatedRegulation",
                column: "RelatedRegId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                    name: "FK_LinkedRelatedRegulation_Regulation_RegId",
                    schema: "Library",
                    table: "LinkedRelatedRegulation"
                );

            migrationBuilder.DropForeignKey(
                    name: "FK_LinkedRelatedRegulation_Regulation_RelatedRegId",
                    schema: "Library",
                    table: "LinkedRelatedRegulation"
                );

            migrationBuilder.DropIndex(
                name: "IX_LinkedRelatedRegulation_RegId",
                schema: "Library",
                table: "LinkedRelatedRegulation");

            migrationBuilder.DropIndex(
                name: "IX_LinkedRelatedRegulation_RelatedRegId",
                schema: "Library",
                table: "LinkedRelatedRegulation");

            migrationBuilder.DropTable(
                name: "LinkedRelatedRegulation",
                schema: "Library");
        }
    }
}
