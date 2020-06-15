using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompData.Migrations
{
    public partial class AlterDefaultBaseModelAddedColumnViewRegulation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regulation_ApplicationUser_AddBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropForeignKey(
                name: "FK_Regulation_ApplicationUser_EditBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropIndex(
                name: "IX_Regulation_AddBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropIndex(
                name: "IX_Regulation_EditBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure");

            migrationBuilder.DropColumn(
                name: "SourceId",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure");

            migrationBuilder.DropColumn(
                name: "AddBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "AddIP",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "AddOn",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "EditBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "EditIP",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "EditOn",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Library",
                table: "Regulation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Library",
                table: "Regulation",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                schema: "Library",
                table: "Regulation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                schema: "Library",
                table: "Regulation",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Views",
                schema: "Library",
                table: "Regulation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_CreatedBy",
                schema: "Library",
                table: "Regulation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_UpdatedBy",
                schema: "Library",
                table: "Regulation",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Regulation_ApplicationUser_CreatedBy",
                schema: "Library",
                table: "Regulation",
                column: "CreatedBy",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regulation_ApplicationUser_UpdatedBy",
                schema: "Library",
                table: "Regulation",
                column: "UpdatedBy",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regulation_ApplicationUser_CreatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropForeignKey(
                name: "FK_Regulation_ApplicationUser_UpdatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropIndex(
                name: "IX_Regulation_CreatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropIndex(
                name: "IX_Regulation_UpdatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure");

            migrationBuilder.DropColumn(
                name: "TypeName",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.DropColumn(
                name: "Views",
                schema: "Library",
                table: "Regulation");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                schema: "ProcedureView",
                table: "RegulationGroupBySourceProcedure",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AddBy",
                schema: "Library",
                table: "Regulation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddIP",
                schema: "Library",
                table: "Regulation",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddOn",
                schema: "Library",
                table: "Regulation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EditBy",
                schema: "Library",
                table: "Regulation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditIP",
                schema: "Library",
                table: "Regulation",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditOn",
                schema: "Library",
                table: "Regulation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_AddBy",
                schema: "Library",
                table: "Regulation",
                column: "AddBy");

            migrationBuilder.CreateIndex(
                name: "IX_Regulation_EditBy",
                schema: "Library",
                table: "Regulation",
                column: "EditBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Regulation_ApplicationUser_AddBy",
                schema: "Library",
                table: "Regulation",
                column: "AddBy",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regulation_ApplicationUser_EditBy",
                schema: "Library",
                table: "Regulation",
                column: "EditBy",
                principalSchema: "Security",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
