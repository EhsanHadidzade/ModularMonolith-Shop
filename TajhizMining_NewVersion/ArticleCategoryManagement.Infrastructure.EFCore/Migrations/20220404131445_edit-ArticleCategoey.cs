using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleCategoryManagement.Infrastructure.EFCore.Migrations
{
    public partial class editArticleCategoey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureAlt",
                table: "ArticleCagetories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureTitle",
                table: "ArticleCagetories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureAlt",
                table: "ArticleCagetories");

            migrationBuilder.DropColumn(
                name: "PictureTitle",
                table: "ArticleCagetories");
        }
    }
}
