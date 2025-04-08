using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Township_API.Migrations
{
    /// <inheritdoc />
    public partial class codetypesupdate_19pm21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_[tblProfile]_[tblUser]_userId",
                table: "[tblProfile]");

            migrationBuilder.DropForeignKey(
                name: "FK_[tblUser]_[tblRole]_RoleID",
                table: "[tblUser]");

            migrationBuilder.DropPrimaryKey(
                name: "PK_[tblUser]",
                table: "[tblUser]");

            migrationBuilder.RenameTable(
                name: "[tblUser]",
                newName: "tblUser");

            migrationBuilder.RenameIndex(
                name: "IX_[tblUser]_RoleID",
                table: "tblUser",
                newName: "IX_tblUser_RoleID");

            migrationBuilder.AddColumn<int>(
                name: "createdby",
                table: "tblUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdon",
                table: "tblUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "tblUser",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tblUser",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "updatedby",
                table: "tblUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedon",
                table: "tblUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblUser",
                table: "tblUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_[tblProfile]_tblUser_userId",
                table: "[tblProfile]",
                column: "userId",
                principalTable: "tblUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_[tblRole]_RoleID",
                table: "tblUser",
                column: "RoleID",
                principalTable: "[tblRole]",
                principalColumn: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_[tblProfile]_tblUser_userId",
                table: "[tblProfile]");

            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_[tblRole]_RoleID",
                table: "tblUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblUser",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "createdon",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "updatedby",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "updatedon",
                table: "tblUser");

            migrationBuilder.RenameTable(
                name: "tblUser",
                newName: "[tblUser]");

            migrationBuilder.RenameIndex(
                name: "IX_tblUser_RoleID",
                table: "[tblUser]",
                newName: "IX_[tblUser]_RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_[tblUser]",
                table: "[tblUser]",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_[tblProfile]_[tblUser]_userId",
                table: "[tblProfile]",
                column: "userId",
                principalTable: "[tblUser]",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_[tblUser]_[tblRole]_RoleID",
                table: "[tblUser]",
                column: "RoleID",
                principalTable: "[tblRole]",
                principalColumn: "RoleID");
        }
    }
}
