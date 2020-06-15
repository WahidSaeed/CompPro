using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedColumnSourceIdLinkUserRegTypSubscriptionContraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LinkUserRegTypeSubscription_RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription",
                column: "RegSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkUserRegTypeSubscription_RegulationSource_RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription",
                column: "RegSourceId",
                principalSchema: "Library",
                principalTable: "RegulationSource",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkUserRegTypeSubscription_RegulationSource_RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription");

            migrationBuilder.DropIndex(
                name: "IX_LinkUserRegTypeSubscription_RegSourceId",
                schema: "Library",
                table: "LinkUserRegTypeSubscription");
        }
    }
}
