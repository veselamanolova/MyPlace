using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlace.Data.Migrations
{
    public partial class AddSelfRelationEntityToParentEstablishment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstablishmentId",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCommentable",
                table: "Entities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_EstablishmentId",
                table: "Entities",
                column: "EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_EstablishmentId",
                table: "Entities",
                column: "EstablishmentId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_EstablishmentId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_EstablishmentId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "EstablishmentId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "IsCommentable",
                table: "Entities");
        }
    }
}
