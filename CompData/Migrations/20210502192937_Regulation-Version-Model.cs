using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class RegulationVersionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkedRelatedRegulationVersion",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkId = table.Column<int>(nullable: false),
                    RegId = table.Column<int>(nullable: false),
                    RelatedRegId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedRelatedRegulationVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegulationDetailVersion",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<string>(nullable: true),
                    RegDetailId = table.Column<int>(nullable: false),
                    RegDescription = table.Column<string>(nullable: true),
                    RegDescriptionClean = table.Column<string>(nullable: true),
                    RegulationId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    VersionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationDetailVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegulationSectionVersion",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(nullable: false),
                    SectionTitle = table.Column<string>(maxLength: 500, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    RegulationId = table.Column<int>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    VersionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationSectionVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagMapVersion",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(nullable: false),
                    TagGroupKey = table.Column<string>(maxLength: 250, nullable: false),
                    Tag = table.Column<string>(maxLength: 250, nullable: false),
                    RegId = table.Column<int>(nullable: false),
                    SecId = table.Column<int>(nullable: false),
                    DescId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMapVersion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedRelatedRegulationVersion",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "RegulationDetailVersion",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "RegulationSectionVersion",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "TagMapVersion",
                schema: "Library");
        }
    }
}
