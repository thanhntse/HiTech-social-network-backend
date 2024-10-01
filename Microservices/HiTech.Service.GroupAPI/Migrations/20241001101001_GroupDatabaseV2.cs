using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.GroupAPI.Migrations
{
    /// <inheritdoc />
    public partial class GroupDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "join_request",
                schema: "dbo",
                columns: table => new
                {
                    join_request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_join_request", x => x.join_request_id);
                    table.ForeignKey(
                        name: "FK_join_request_group_group_id",
                        column: x => x.group_id,
                        principalSchema: "dbo",
                        principalTable: "group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_join_request_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_join_request_group_id",
                schema: "dbo",
                table: "join_request",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_join_request_user_id",
                schema: "dbo",
                table: "join_request",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "join_request",
                schema: "dbo");
        }
    }
}
