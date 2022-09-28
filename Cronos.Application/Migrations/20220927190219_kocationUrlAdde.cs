using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cronos.Application.Migrations
{
    public partial class kocationUrlAdde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "location",
                table: "Activities",
                newName: "locationUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "locationUrl",
                table: "Activities",
                newName: "location");
        }
    }
}
