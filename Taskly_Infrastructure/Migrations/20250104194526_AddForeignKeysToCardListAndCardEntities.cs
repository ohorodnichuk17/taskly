using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToCardListAndCardEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BoardEntityId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardLists_CardListEntityId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardListEntityId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_CardLists_BoardEntityId",
                table: "CardLists");

            migrationBuilder.DropColumn(
                name: "CardListEntityId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BoardEntityId",
                table: "CardLists");

            migrationBuilder.AddColumn<Guid>(
                name: "CardListId",
                table: "Cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "CardLists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListId",
                table: "Cards",
                column: "CardListId");

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BoardId",
                table: "CardLists",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardLists_CardListId",
                table: "Cards",
                column: "CardListId",
                principalTable: "CardLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BoardId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardLists_CardListId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardListId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists");

            migrationBuilder.DropColumn(
                name: "CardListId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "CardLists");

            migrationBuilder.AddColumn<Guid>(
                name: "CardListEntityId",
                table: "Cards",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BoardEntityId",
                table: "CardLists",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListEntityId",
                table: "Cards",
                column: "CardListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardEntityId",
                table: "CardLists",
                column: "BoardEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BoardEntityId",
                table: "CardLists",
                column: "BoardEntityId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardLists_CardListEntityId",
                table: "Cards",
                column: "CardListEntityId",
                principalTable: "CardLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
