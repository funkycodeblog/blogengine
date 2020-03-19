using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunkyCode.Blog.Inf.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PublishingDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: false, defaultValue: "New"),
                    Header = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true),
                    BlogPostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImages_BlogPostId",
                table: "BlogPostImages",
                column: "BlogPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostImages");

            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}
