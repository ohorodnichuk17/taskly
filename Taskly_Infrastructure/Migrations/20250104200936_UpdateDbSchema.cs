using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_TimeRangeEntity_Id",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRangeEntity",
                table: "TimeRangeEntity");

            migrationBuilder.RenameTable(
                name: "TimeRangeEntity",
                newName: "TimeRanges");

            migrationBuilder.AddColumn<Guid>(
                name: "TimeRangeEntityId",
                table: "Cards",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRanges",
                table: "TimeRanges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TimeRangeEntityId",
                table: "Cards",
                column: "TimeRangeEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_TimeRanges_TimeRangeEntityId",
                table: "Cards",
                column: "TimeRangeEntityId",
                principalTable: "TimeRanges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_TimeRanges_Id",
                table: "ToDoItems",
                column: "Id",
                principalTable: "TimeRanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_TimeRanges_TimeRangeEntityId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_TimeRanges_Id",
                table: "ToDoItems");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TimeRangeEntityId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRanges",
                table: "TimeRanges");

            migrationBuilder.DropColumn(
                name: "TimeRangeEntityId",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "TimeRanges",
                newName: "TimeRangeEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRangeEntity",
                table: "TimeRangeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards",
                column: "Id",
                principalTable: "TimeRangeEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_TimeRangeEntity_Id",
                table: "ToDoItems",
                column: "Id",
                principalTable: "TimeRangeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
