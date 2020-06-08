using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class procedureentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ProcedureView");

            migrationBuilder.CreateTable(
                name: "RegulationGroupBySourceProcedure",
                schema: "ProcedureView",
                columns: table => new
                {
                    SourceId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    RegId = table.Column<int>(nullable: false),
                    RegTitle = table.Column<string>(nullable: true),
                    Regcount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserAccessibleClaims",
                schema: "ProcedureView",
                columns: table => new
                {
                    DisplayName = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    MenuID = table.Column<int>(nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    IsLocal = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegulationGroupBySourceProcedure",
                schema: "ProcedureView");

            migrationBuilder.DropTable(
                name: "UserAccessibleClaims",
                schema: "ProcedureView");
        }
    }
}
