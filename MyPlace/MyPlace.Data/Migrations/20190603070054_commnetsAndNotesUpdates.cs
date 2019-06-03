using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlace.Data.Migrations
{
    public partial class commnetsAndNotesUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hasStatus",
                table: "Notes",
                newName: "HasStatus");

            migrationBuilder.AlterColumn<bool>(
                name: "HasStatus",
                table: "Notes",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasStatus",
                table: "Notes",
                newName: "hasStatus");

            migrationBuilder.AlterColumn<bool>(
                name: "hasStatus",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
