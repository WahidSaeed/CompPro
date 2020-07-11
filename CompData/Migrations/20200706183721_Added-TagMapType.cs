using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedTagMapType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagType",
                schema: "Library",
                table: "TagMap",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TagMapType",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMapType", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Library",
                table: "TagMapType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "DetailTag" }
            );

            migrationBuilder.InsertData(
                schema: "Library",
                table: "TagMapType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "BussinessLineTag" }
            );

            migrationBuilder.UpdateData(
                schema: "Library",
                table: "TagMap",
                keyColumn: "TagType",
                keyValue: "0",
                column: "TagType",
                value: 1
            );

            migrationBuilder.CreateIndex(
                name: "IX_TagMap_TagType",
                schema: "Library",
                table: "TagMap",
                column: "TagType");

            migrationBuilder.AddForeignKey(
                name: "FK_TagMap_TagMapType_TagType",
                schema: "Library",
                table: "TagMap",
                column: "TagType",
                principalSchema: "Library",
                principalTable: "TagMapType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagMap_TagMapType_TagType",
                schema: "Library",
                table: "TagMap");

            migrationBuilder.DropTable(
                name: "TagMapType",
                schema: "Library");

            migrationBuilder.DropIndex(
                name: "IX_TagMap_TagType",
                schema: "Library",
                table: "TagMap");

            migrationBuilder.DropColumn(
                name: "TagType",
                schema: "Library",
                table: "TagMap");
        }
    }
}
