using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedTagMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagMap",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagGroupKey = table.Column<string>(maxLength: 250, nullable: false),
                    Tag = table.Column<string>(maxLength: 250, nullable: false),
                    RegId = table.Column<int>(nullable: false),
                    SecId = table.Column<int>(nullable: false),
                    DescId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagMap_RegulationDetail_DescId",
                        column: x => x.DescId,
                        principalSchema: "Library",
                        principalTable: "RegulationDetail",
                        principalColumn: "RegDetailId");
                    table.ForeignKey(
                        name: "FK_TagMap_Regulation_RegId",
                        column: x => x.RegId,
                        principalSchema: "Library",
                        principalTable: "Regulation",
                        principalColumn: "RegId");
                    table.ForeignKey(
                        name: "FK_TagMap_RegulationSection_SecId",
                        column: x => x.SecId,
                        principalSchema: "Library",
                        principalTable: "RegulationSection",
                        principalColumn: "SectionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_DescId",
                schema: "Library",
                table: "TagMap",
                column: "DescId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_RegId",
                schema: "Library",
                table: "TagMap",
                column: "RegId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_SecId",
                schema: "Library",
                table: "TagMap",
                column: "SecId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_TagGroupKey",
                schema: "Library",
                table: "TagMap",
                column: "TagGroupKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagMap",
                schema: "Library");
        }
    }
}
