using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardTeamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardTeamEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LeaderOfBoardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardTeamEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardTeamEntity_Boards_Id",
                        column: x => x.Id,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardTeamEntityUserEntity");

            migrationBuilder.DropTable(
                name: "BoardTeamEntity");
        }
    }
}
