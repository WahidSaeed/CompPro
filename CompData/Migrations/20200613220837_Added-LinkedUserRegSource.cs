using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedLinkedUserRegSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedUserRegulationSource",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedUserRegulationSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedUserRegulationSource_RegulationSource_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Library",
                        principalTable: "RegulationSource",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkedUserRegulationSource_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkedUserRegulationSource_SourceId",
                schema: "Library",
                table: "LinkedUserRegulationSource",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedUserRegulationSource_UserId",
                schema: "Library",
                table: "LinkedUserRegulationSource",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedUserRegulationSource",
                schema: "Library");
        }
    }
}
