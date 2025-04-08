using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Township_API.Migrations
{
    /// <inheritdoc />
    public partial class CodeModule_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Buildings");

            //migrationBuilder.DropTable(
            //    name: "NRDs");

            //migrationBuilder.AddColumn<string>(
            //    name: "Discriminator",
            //    table: "tblModuleData",
            //    type: "nvarchar(21)",
            //    maxLength: 21,
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.CreateTable(
            //    name: "tblServiceProvider",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        code = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ServiceProviderID = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        role = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        isactive = table.Column<bool>(type: "bit", nullable: true),
            //        createdby = table.Column<int>(type: "int", nullable: true),
            //        createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        updatedby = table.Column<int>(type: "int", nullable: true),
            //        updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblServiceProvider", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblVehicle",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RegNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        vType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        vMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        vColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TagUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PrintedTagID = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TagEncodingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Logical_Delete = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StickerNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        isactive = table.Column<bool>(type: "bit", nullable: true),
            //        createdby = table.Column<int>(type: "int", nullable: true),
            //        createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        updatedby = table.Column<int>(type: "int", nullable: true),
            //        updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblVehicle", x => x.ID);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "tblServiceProvider");

            //migrationBuilder.DropTable(
            //    name: "tblVehicle");

            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "tblModuleData");

            //migrationBuilder.CreateTable(
            //    name: "NRDs",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NRDs", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_NRDs_tblModuleData_ID",
            //            column: x => x.ID,
            //            principalTable: "tblModuleData",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Buildings",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Buildings", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Buildings_NRDs_ID",
            //            column: x => x.ID,
            //            principalTable: "NRDs",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });
        }
    }
}
