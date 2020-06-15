using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedCountryRegTypeAppSettingOrgDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Config");

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                schema: "Library",
                table: "RegulationSource",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppSetting",
                schema: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 50, nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Config",
                columns: table => new
                {
                    CountryId = table.Column<string>(maxLength: 3, nullable: false),
                    CountryName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationDomain",
                schema: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationDomain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegulationType",
                schema: "Library",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    TypeCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationType", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegulationSource_CountryId",
                schema: "Library",
                table: "RegulationSource",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_RegTypeID",
                schema: "Library",
                table: "Regulation",
                column: "RegTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Regulation_RegulationType_RegTypeID",
                schema: "Library",
                table: "Regulation",
                column: "RegTypeID",
                principalSchema: "Library",
                principalTable: "RegulationType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationSource_Country_CountryId",
                schema: "Library",
                table: "RegulationSource",
                column: "CountryId",
                principalSchema: "Config",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regulation_RegulationType_RegTypeID",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropForeignKey(
                name: "FK_RegulationSource_Country_CountryId",
                schema: "Library",
                table: "RegulationSource");

            migrationBuilder.DropTable(
                name: "AppSetting",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "OrganizationDomain",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "RegulationType",
                schema: "Library");

            migrationBuilder.DropIndex(
                name: "IX_RegulationSource_CountryId",
                schema: "Library",
                table: "RegulationSource");

            migrationBuilder.DropIndex(
                name: "IX_Regulation_RegTypeID",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "Library",
                table: "RegulationSource");
        }
    }
}
