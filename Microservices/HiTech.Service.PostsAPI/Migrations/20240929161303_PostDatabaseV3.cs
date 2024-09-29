using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.PostsAPI.Migrations
{
    /// <inheritdoc />
    public partial class PostDatabaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "dbo",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                schema: "dbo",
                table: "user",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "dbo",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_login",
                schema: "dbo",
                table: "user");
        }
    }
}
