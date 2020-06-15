using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedColumnSourceIdLinkUserRegTypSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription");
        }
    }
}
