using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlace.Data.Migrations
{
    public partial class dbremastered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Entities",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Comments",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Entities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommentReply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CommentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReply_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReply_CommentId",
                table: "CommentReply",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReply");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Entities");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Entities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Comments",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Text");
        }
    }
}
