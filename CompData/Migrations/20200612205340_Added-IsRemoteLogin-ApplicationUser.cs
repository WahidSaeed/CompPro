using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AddedIsRemoteLoginApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserNotification",
                schema: "Security");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowRemoteLogin",
                schema: "Security",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowRemoteLogin",
                schema: "Security",
                table: "ApplicationUser");

            migrationBuilder.CreateTable(
                name: "ApplicationUserNotification",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Notification = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageRedirect = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
