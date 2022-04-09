using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleCategoryManagement.Infrastructure.EFCore.Migrations
{
    public partial class init_ArticleCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCagetories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShowOrder = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KeyWords = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CanonicalAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CretionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCagetories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCagetories");
        }
    }
}
