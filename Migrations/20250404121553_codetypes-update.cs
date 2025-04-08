using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Township_API.Migrations
{
    /// <inheritdoc />
    public partial class codetypesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "ModuleType",
            //    table: "tblModuleData",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(21)",
            //    oldMaxLength: 21);

            //migrationBuilder.AddColumn<string>(
            //    name: "Discriminator",
            //    table: "tblModuleData",
            //    type: "nvarchar(21)",
            //    maxLength: 21,
            //    nullable: false,
            //    defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "tblModuleData");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ModuleType",
            //    table: "tblModuleData",
            //    type: "nvarchar(21)",
            //    maxLength: 21,
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);
        }
    }
}
