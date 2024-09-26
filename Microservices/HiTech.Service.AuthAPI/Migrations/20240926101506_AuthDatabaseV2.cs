using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "background",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "background",
                schema: "dbo",
                table: "account");
        }
    }
}
