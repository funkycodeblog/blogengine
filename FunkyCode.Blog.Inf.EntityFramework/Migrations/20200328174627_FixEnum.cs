using Microsoft.EntityFrameworkCore.Migrations;

namespace FunkyCode.Blog.Inf.EntityFramework.Migrations
{
    public partial class FixEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BlogPosts",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "New");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BlogPosts",
                nullable: false,
                defaultValue: "New",
                oldClrType: typeof(string));
        }
    }
}
