using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddLinkedUserRegulationSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkUserRegulationSubscription",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    RegId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkUserRegulationSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkUserRegulationSubscription_Regulation_RegId",
                        column: x => x.RegId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkUserRegulationSubscription_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRegulationSubscription_RegId",
                schema: "Library",
                table: "LinkUserRegulationSubscription",
                column: "RegId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRegulationSubscription_UserId",
                schema: "Library",
                table: "LinkUserRegulationSubscription",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkUserRegulationSubscription",
                schema: "Library");
        }
    }
}
