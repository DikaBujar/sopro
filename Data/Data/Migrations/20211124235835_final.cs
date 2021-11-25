using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_CategoryId",
                table: "Bookmark");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_CategoryId",
                table: "Bookmark",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
