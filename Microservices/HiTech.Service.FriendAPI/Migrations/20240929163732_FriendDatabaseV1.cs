using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiTech.Service.FriendAPI.Migrations
{
    /// <inheritdoc />
    public partial class FriendDatabaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "friend_request",
                schema: "dbo",
                columns: table => new
                {
                    friend_request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<int>(type: "int", nullable: false),
                    receiver_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friend_request", x => x.friend_request_id);
                    table.CheckConstraint("CK_Status_Valid", "[status] IN ('Pending', 'Accepted', 'Denied')");
                    table.ForeignKey(
                        name: "FK_friend_request_user_receiver_id",
                        column: x => x.receiver_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friend_request_user_sender_id",
                        column: x => x.sender_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "friendship",
                schema: "dbo",
                columns: table => new
                {
                    friendship_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_sent_id = table.Column<int>(type: "int", nullable: false),
                    user_received_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendship", x => x.friendship_id);
                    table.ForeignKey(
                        name: "FK_friendship_user_user_received_id",
                        column: x => x.user_received_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friendship_user_user_sent_id",
                        column: x => x.user_sent_id,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friend_request_receiver_id",
                schema: "dbo",
                table: "friend_request",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_friend_request_sender_id",
                schema: "dbo",
                table: "friend_request",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_friendship_user_received_id",
                schema: "dbo",
                table: "friendship",
                column: "user_received_id");

            migrationBuilder.CreateIndex(
                name: "IX_friendship_user_sent_id",
                schema: "dbo",
                table: "friendship",
                column: "user_sent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friend_request",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "friendship",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user",
                schema: "dbo");
        }
    }
}
