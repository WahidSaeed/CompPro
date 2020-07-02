using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddTableComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
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
                    CommentTypeID = table.Column<int>(nullable: false),
                    ParentID = table.Column<int>(nullable: false),
                    RegID = table.Column<int>(nullable: true),
                    RegDetailID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_ApplicationUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_RegulationDetail_RegDetailID",
                        column: x => x.RegDetailID,
                        principalSchema: "Library",
                        principalTable: "RegulationDetail",
                        principalColumn: "RegDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Regulation_RegID",
                        column: x => x.RegID,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_ApplicationUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedBy",
                schema: "Library",
                table: "Comments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RegDetailID",
                schema: "Library",
                table: "Comments",
                column: "RegDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RegID",
                schema: "Library",
                table: "Comments",
                column: "RegID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UpdatedBy",
                schema: "Library",
                table: "Comments",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "Library");
        }
    }
}
