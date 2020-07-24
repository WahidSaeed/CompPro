using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddTableRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requirements",
                schema: "Library",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    CommentText = table.Column<string>(nullable: true),
                    RegID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Requirements_ApplicationUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requirements_Regulation_RegID",
                        column: x => x.RegID,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requirements_ApplicationUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_CreatedBy",
                schema: "Library",
                table: "Requirements",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_RegID",
                schema: "Library",
                table: "Requirements",
                column: "RegID");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_UpdatedBy",
                schema: "Library",
                table: "Requirements",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requirements",
                schema: "Library");
        }
    }
}
