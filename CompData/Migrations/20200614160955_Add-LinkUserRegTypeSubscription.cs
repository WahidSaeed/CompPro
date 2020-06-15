using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddLinkUserRegTypeSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkUserRegTypeSubscription",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    RegTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkUserRegTypeSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkUserRegTypeSubscription_RegulationType_RegTypeId",
                        column: x => x.RegTypeId,
                        principalSchema: "Library",
                        principalTable: "RegulationType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkUserRegTypeSubscription_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRegTypeSubscription_RegTypeId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription",
                column: "RegTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRegTypeSubscription_UserId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkUserRegTypeSubscription",
                schema: "Library");
        }
    }
}
