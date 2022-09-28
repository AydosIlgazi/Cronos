using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cronos.Application.Migrations
{
    public partial class removedOrdersInMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubMenuOrder",
                table: "SubMenus2");

            migrationBuilder.DropColumn(
                name: "SubMenuOrder",
                table: "SubMenus");

            migrationBuilder.DropColumn(
                name: "MenuOrder",
                table: "Menus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubMenuOrder",
                table: "SubMenus2",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubMenuOrder",
                table: "SubMenus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuOrder",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
