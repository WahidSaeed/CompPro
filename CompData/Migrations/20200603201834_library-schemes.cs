using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class libraryschemes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Library");

            migrationBuilder.CreateTable(
                name: "RegulationSource",
                schema: "Library",
                columns: table => new
                {
                    SourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationSource", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "Regulation",
                schema: "Library",
                columns: table => new
                {
                    RegId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegulationTitle = table.Column<string>(maxLength: 250, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    SourceID = table.Column<int>(nullable: false),
                    ReferenceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    RegTypeID = table.Column<int>(nullable: false),
                    AddBy = table.Column<Guid>(nullable: true),
                    AddIP = table.Column<string>(maxLength: 15, nullable: true),
                    AddOn = table.Column<DateTime>(nullable: true),
                    EditBy = table.Column<Guid>(nullable: true),
                    EditOn = table.Column<DateTime>(nullable: true),
                    EditIP = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regulation", x => x.RegId);
                    table.ForeignKey(
                        name: "FK_Regulation_ApplicationUser_AddBy",
                        column: x => x.AddBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regulation_ApplicationUser_EditBy",
                        column: x => x.EditBy,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regulation_RegulationSource_SourceID",
                        column: x => x.SourceID,
                        principalSchema: "Library",
                        principalTable: "RegulationSource",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegulationSection",
                schema: "Library",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionTitle = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    RegulationId = table.Column<int>(nullable: false),
                    Sequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationSection", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_RegulationSection_RegulationSection_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Library",
                        principalTable: "RegulationSection",
                        principalColumn: "SectionId");
                    table.ForeignKey(
                        name: "FK_RegulationSection_Regulation_RegulationId",
                        column: x => x.RegulationId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegulationDetail",
                schema: "Library",
                columns: table => new
                {
                    RegDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDescription = table.Column<string>(nullable: true),
                    RegulationId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationDetail", x => x.RegDetailId);
                    table.ForeignKey(
                        name: "FK_RegulationDetail_Regulation_RegulationId",
                        column: x => x.RegulationId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegulationDetail_RegulationSection_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "Library",
                        principalTable: "RegulationSection",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_AddBy",
                schema: "Library",
                table: "Regulation",
                column: "AddBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_EditBy",
                schema: "Library",
                table: "Regulation",
                column: "EditBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_SourceID",
                schema: "Library",
                table: "Regulation",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationDetail_RegulationId",
                schema: "Library",
                table: "RegulationDetail",
                column: "RegulationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationDetail_SectionId",
                schema: "Library",
                table: "RegulationDetail",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationSection_ParentId",
                schema: "Library",
                table: "RegulationSection",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulationSection_RegulationId",
                schema: "Library",
                table: "RegulationSection",
                column: "RegulationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegulationDetail",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "RegulationSection",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Regulation",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "RegulationSource",
                schema: "Library");
        }
    }
}
