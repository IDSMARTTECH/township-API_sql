using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Township_API.Migrations
{
    /// <inheritdoc />
    public partial class ModuleDataCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblModuleData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeID = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    isactive = table.Column<bool>(type: "bit", nullable: true),
                    createdby = table.Column<int>(type: "int", nullable: true),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedby = table.Column<int>(type: "int", nullable: true),
                    updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblModuleData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NRDs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NRDs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NRDs_tblModuleData_ID",
                        column: x => x.ID,
                        principalTable: "tblModuleData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buildings_NRDs_ID",
                        column: x => x.ID,
                        principalTable: "NRDs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "NRDs");

            migrationBuilder.DropTable(
                name: "tblModuleData");
        }
    }
}
