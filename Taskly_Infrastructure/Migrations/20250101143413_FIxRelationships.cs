using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FIxRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardTeamEntity_Boards_Id",
                table: "BoardTeamEntity");

            migrationBuilder.DropTable(
                name: "BoardTeamEntityUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardTeamEntity",
                table: "BoardTeamEntity");

            migrationBuilder.RenameTable(
                name: "BoardTeamEntity",
                newName: "BoardTeams");

            migrationBuilder.RenameColumn(
                name: "LeaderOfBoardId",
                table: "BoardTeams",
                newName: "IdOfBoardLeader");

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

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "BoardTeams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardTeams",
                table: "BoardTeams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardTeamId",
                table: "Boards",
                column: "BoardTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BoardTeamEntityId",
                table: "AspNetUsers",
                column: "BoardTeamEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardTeams_BoardId",
                table: "BoardTeams",
                column: "BoardId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BoardTeams_Boards_BoardId",
                table: "BoardTeams",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BoardTeams_BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_BoardTeams_BoardTeamId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardTeams_Boards_BoardId",
                table: "BoardTeams");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BoardTeamId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardTeams",
                table: "BoardTeams");

            migrationBuilder.DropIndex(
                name: "IX_BoardTeams_BoardId",
                table: "BoardTeams");

            migrationBuilder.DropColumn(
                name: "BoardTeamId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardTeamEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "BoardTeams");

            migrationBuilder.RenameTable(
                name: "BoardTeams",
                newName: "BoardTeamEntity");

            migrationBuilder.RenameColumn(
                name: "IdOfBoardLeader",
                table: "BoardTeamEntity",
                newName: "LeaderOfBoardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardTeamEntity",
                table: "BoardTeamEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BoardTeamEntityUserEntity",
                columns: table => new
                {
                    BoardTeamEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardTeamEntityUserEntity", x => new { x.BoardTeamEntityId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_BoardTeamEntityUserEntity_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardTeamEntityUserEntity_BoardTeamEntity_BoardTeamEntityId",
                        column: x => x.BoardTeamEntityId,
                        principalTable: "BoardTeamEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardTeamEntityUserEntity_MembersId",
                table: "BoardTeamEntityUserEntity",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardTeamEntity_Boards_Id",
                table: "BoardTeamEntity",
                column: "Id",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
