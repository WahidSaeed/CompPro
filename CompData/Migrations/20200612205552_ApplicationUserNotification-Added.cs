using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class ApplicationUserNotificationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserNotification",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notification = table.Column<string>(maxLength: 250, nullable: false),
                    FK_UserId = table.Column<Guid>(nullable: false),
                    PageRedirect = table.Column<string>(nullable: false),
                    NotificationDate = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserNotification_ApplicationUser_FK_UserId",
                        column: x => x.FK_UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserNotification_FK_UserId",
                schema: "Security",
                table: "ApplicationUserNotification",
                column: "FK_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserNotification",
                schema: "Security");
        }
    }
}
