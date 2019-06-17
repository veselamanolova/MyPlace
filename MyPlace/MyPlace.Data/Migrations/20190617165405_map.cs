using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlace.Data.Migrations
{
    public partial class map : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationLatitude",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationLongitude",
                table: "Entities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationLatitude",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "LocationLongitude",
                table: "Entities");
        }
    }
}
