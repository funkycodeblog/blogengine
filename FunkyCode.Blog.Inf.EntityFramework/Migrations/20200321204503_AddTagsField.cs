using Microsoft.EntityFrameworkCore.Migrations;

namespace FunkyCode.Blog.Inf.EntityFramework.Migrations
{
    public partial class AddTagsField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BlogPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BlogPosts");
        }
    }
}
