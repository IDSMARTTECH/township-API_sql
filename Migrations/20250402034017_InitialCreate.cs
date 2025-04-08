using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Township_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "[tblModules]",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[tblModules]", x => x.ModuleID);
                });

            migrationBuilder.CreateTable(
                name: "[tblRole]",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[tblRole]", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "[tblUser]",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[tblUser]", x => x.Id);
                    table.ForeignKey(
                        name: "FK_[tblUser]_[tblRole]_RoleID",
                        column: x => x.RoleID,
                        principalTable: "[tblRole]",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "[tblProfile]",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profilename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    uid = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: true),
                    isactive = table.Column<bool>(type: "bit", nullable: true),
                    isdeleted = table.Column<bool>(type: "bit", nullable: true),
                    createdby = table.Column<int>(type: "int", nullable: true),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedby = table.Column<int>(type: "int", nullable: true),
                    updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[tblProfile]", x => x.ID);
                    table.ForeignKey(
                        name: "FK_[tblProfile]_[tblUser]_userId",
                        column: x => x.userId,
                        principalTable: "[tblUser]",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "[tblProfileDetails]",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profileid = table.Column<int>(type: "int", nullable: true),
                    moduleId = table.Column<int>(type: "int", nullable: true),
                    CanInsert = table.Column<bool>(type: "bit", nullable: true),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: true),
                    CanDelete = table.Column<bool>(type: "bit", nullable: true),
                    CanView = table.Column<bool>(type: "bit", nullable: true),
                    isactive = table.Column<bool>(type: "bit", nullable: true),
                    isdeleted = table.Column<bool>(type: "bit", nullable: true),
                    createdby = table.Column<int>(type: "int", nullable: true),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedby = table.Column<int>(type: "int", nullable: true),
                    updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[tblProfileDetails]", x => x.id);
                    table.ForeignKey(
                        name: "FK_[tblProfileDetails]_[tblModules]_moduleId",
                        column: x => x.moduleId,
                        principalTable: "[tblModules]",
                        principalColumn: "ModuleID");
                    table.ForeignKey(
                        name: "FK_[tblProfileDetails]_[tblProfile]_profileid",
                        column: x => x.profileid,
                        principalTable: "[tblProfile]",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_[tblProfile]_userId",
                table: "[tblProfile]",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_[tblProfileDetails]_moduleId",
                table: "[tblProfileDetails]",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "IX_[tblProfileDetails]_profileid",
                table: "[tblProfileDetails]",
                column: "profileid");

            migrationBuilder.CreateIndex(
                name: "IX_[tblUser]_RoleID",
                table: "[tblUser]",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "[tblProfileDetails]");

            migrationBuilder.DropTable(
                name: "[tblModules]");

            migrationBuilder.DropTable(
                name: "[tblProfile]");

            migrationBuilder.DropTable(
                name: "[tblUser]");

            migrationBuilder.DropTable(
                name: "[tblRole]");
        }
    }
}
