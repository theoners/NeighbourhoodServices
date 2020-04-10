using Microsoft.EntityFrameworkCore.Migrations;

namespace NeighbourhoodServices.Data.Migrations
{
    public partial class updateSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Announcements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Announcements");
        }
    }
}
