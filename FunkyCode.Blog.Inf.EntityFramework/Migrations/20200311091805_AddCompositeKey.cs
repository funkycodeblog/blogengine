using Microsoft.EntityFrameworkCore.Migrations;

namespace FunkyCode.Blog.Inf.EntityFramework.Migrations
{
    public partial class AddCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                table: "BlogPostImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostImages",
                table: "BlogPostImages");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostImages_BlogPostId",
                table: "BlogPostImages");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "BlogPostImages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostImages",
                table: "BlogPostImages",
                columns: new[] { "BlogPostId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                table: "BlogPostImages",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                table: "BlogPostImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostImages",
                table: "BlogPostImages");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "BlogPostImages",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostImages",
                table: "BlogPostImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImages_BlogPostId",
                table: "BlogPostImages",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostImages_BlogPosts_BlogPostId",
                table: "BlogPostImages",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
