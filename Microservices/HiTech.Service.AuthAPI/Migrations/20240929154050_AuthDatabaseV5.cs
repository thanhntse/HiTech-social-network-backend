using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDatabaseV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Phone_Valid",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "address",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "background",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "bio",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "phone",
                schema: "dbo",
                table: "account");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "dbo",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                schema: "dbo",
                table: "account",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "account_info",
                schema: "dbo",
                columns: table => new
                {
                    account_info_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    other_info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_info", x => x.account_info_id);
                    table.CheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");
                    table.ForeignKey(
                        name: "FK_account_info_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_email",
                schema: "dbo",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_info_account_id",
                schema: "dbo",
                table: "account_info",
                column: "account_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_info",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_account_email",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "last_login",
                schema: "dbo",
                table: "account");

            migrationBuilder.AddColumn<string>(
                name: "address",
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

            migrationBuilder.AddColumn<string>(
                name: "bio",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                schema: "dbo",
                table: "account",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Phone_Valid",
                schema: "dbo",
                table: "account",
                sql: "LEN([phone]) = 10");
        }
    }
}
