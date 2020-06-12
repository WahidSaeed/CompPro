using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class NewUserLoggingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationIP",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IP = table.Column<string>(nullable: true),
                    IPType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationIP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationLoginAudit",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_User_ID = table.Column<Guid>(nullable: false),
                    CurrentDateTime = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    AuditType = table.Column<int>(nullable: false),
                    LogInErrorReason = table.Column<string>(nullable: true),
                    AddApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLoginAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationLoginAudit_ApplicationUser_AddApplicationUserId",
                        column: x => x.AddApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPasswordLog",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(nullable: false),
                    FK_UserId = table.Column<Guid>(nullable: false),
                    PasswordLogDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPasswordLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationPasswordLog_ApplicationUser_FK_UserId",
                        column: x => x.FK_UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ApplicationLoginAudit_AddApplicationUserId",
                schema: "Security",
                table: "ApplicationLoginAudit",
                column: "AddApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPasswordLog_FK_UserId",
                schema: "Security",
                table: "ApplicationPasswordLog",
                column: "FK_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserNotification_FK_UserId",
                schema: "Security",
                table: "ApplicationUserNotification",
                column: "FK_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationIP",
                schema: "security");

            migrationBuilder.DropTable(
                name: "ApplicationLoginAudit",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationPasswordLog",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ApplicationUserNotification",
                schema: "Security");

            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "RegulationFilteredBySource",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
