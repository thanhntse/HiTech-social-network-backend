using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthDatabaseV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                schema: "dbo",
                table: "account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "dbo",
                table: "account",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Email_Length",
                schema: "dbo",
                table: "account",
                sql: "LEN([email]) >= 6");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Email_Valid",
                schema: "dbo",
                table: "account",
                sql: "CHARINDEX('@', [email]) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account",
                sql: "LEN([full_name]) >= 6");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Phone_Valid",
                schema: "dbo",
                table: "account",
                sql: "LEN([phone]) = 10");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Role_Valid",
                schema: "dbo",
                table: "account",
                sql: "[role] IN ('Member', 'Admin')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Email_Length",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Email_Valid",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Phone_Valid",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Role_Valid",
                schema: "dbo",
                table: "account");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
