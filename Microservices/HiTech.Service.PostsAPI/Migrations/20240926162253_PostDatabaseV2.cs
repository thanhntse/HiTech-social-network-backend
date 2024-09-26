using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.PostsAPI.Migrations
{
    /// <inheritdoc />
    public partial class PostDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_author_id",
                schema: "dbo",
                table: "post",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_like_author_id",
                schema: "dbo",
                table: "like",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_author_id",
                schema: "dbo",
                table: "comment",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_author_id",
                schema: "dbo",
                table: "comment",
                column: "author_id",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_like_user_author_id",
                schema: "dbo",
                table: "like",
                column: "author_id",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_author_id",
                schema: "dbo",
                table: "post",
                column: "author_id",
                principalSchema: "dbo",
                principalTable: "user",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_author_id",
                schema: "dbo",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_like_user_author_id",
                schema: "dbo",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "FK_post_user_author_id",
                schema: "dbo",
                table: "post");

            migrationBuilder.DropTable(
                name: "user",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_post_author_id",
                schema: "dbo",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_like_author_id",
                schema: "dbo",
                table: "like");

            migrationBuilder.DropIndex(
                name: "IX_comment_author_id",
                schema: "dbo",
                table: "comment");
        }
    }
}
