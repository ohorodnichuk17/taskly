using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoardTemplateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardTemplates_Boards_BoardEntityId",
                table: "BoardTemplates");

            migrationBuilder.DropIndex(
                name: "IX_BoardTemplates_BoardEntityId",
                table: "BoardTemplates");

            migrationBuilder.DropColumn(
                name: "BoardEntityId",
                table: "BoardTemplates");

            migrationBuilder.CreateTable(
                name: "BoardBoardTemplate",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardBoardTemplate", x => new { x.BoardId, x.BoardTemplateId });
                    table.ForeignKey(
                        name: "FK_BoardBoardTemplate_BoardTemplates_BoardTemplateId",
                        column: x => x.BoardTemplateId,
                        principalTable: "BoardTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardBoardTemplate_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardBoardTemplate_BoardTemplateId",
                table: "BoardBoardTemplate",
                column: "BoardTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardBoardTemplate");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardEntityId",
                table: "BoardTemplates",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardTemplates_BoardEntityId",
                table: "BoardTemplates",
                column: "BoardEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardTemplates_Boards_BoardEntityId",
                table: "BoardTemplates",
                column: "BoardEntityId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
