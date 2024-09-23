using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDatabaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "expired_token",
                schema: "dbo",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    invalidation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expired_token", x => x.token);
                    table.ForeignKey(
                        name: "FK_expired_token_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_token",
                schema: "dbo",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_refresh_token_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expired_token_account_id",
                schema: "dbo",
                table: "expired_token",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_account_id",
                schema: "dbo",
                table: "refresh_token",
                column: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expired_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "refresh_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "account",
                schema: "dbo");
        }
    }
}
