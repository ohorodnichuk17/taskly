using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBoardTeamEntityAndUpdateRelationshipsBetweenTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Avatars_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BoardTeams_BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_BoardTeams_BoardTeamId",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "BoardEntityUserEntity");

            migrationBuilder.DropTable(
                name: "BoardTeams");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BoardTeamId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BoardTeamId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsTeamBoard",
                table: "Boards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserBoard",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoard", x => new { x.BoardId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserBoard_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBoard_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoard_UserId",
                table: "UserBoard",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Avatars_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Avatars_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserBoard");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsTeamBoard",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardTeamId",
                table: "Boards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardTeamEntityId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoardEntityUserEntity",
                columns: table => new
                {
                    BoardsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardEntityUserEntity", x => new { x.BoardsId, x.UserEntityId });
                    table.ForeignKey(
                        name: "FK_BoardEntityUserEntity_AspNetUsers_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardEntityUserEntity_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdOfBoardLeader = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardTeams_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardTeamId",
                table: "Boards",
                column: "BoardTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BoardTeamEntityId",
                table: "AspNetUsers",
                column: "BoardTeamEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardEntityUserEntity_UserEntityId",
                table: "BoardEntityUserEntity",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardTeams_BoardId",
                table: "BoardTeams",
                column: "BoardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Avatars_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BoardTeams_BoardTeamEntityId",
                table: "AspNetUsers",
                column: "BoardTeamEntityId",
                principalTable: "BoardTeams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_BoardTeams_BoardTeamId",
                table: "Boards",
                column: "BoardTeamId",
                principalTable: "BoardTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
