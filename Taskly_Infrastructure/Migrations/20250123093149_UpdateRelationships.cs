using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "BoardTemplateId",
                table: "Boards",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardTemplateId",
                table: "Boards",
                column: "BoardTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_BoardTemplates_BoardTemplateId",
                table: "Boards",
                column: "BoardTemplateId",
                principalTable: "BoardTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_BoardTemplates_BoardTemplateId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BoardTemplateId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardTemplateId",
                table: "Boards");

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
