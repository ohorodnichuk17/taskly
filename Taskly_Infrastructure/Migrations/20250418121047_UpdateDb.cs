using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_ToDoTables_ToDoTableId",
                table: "UserTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTableItem_ToDoItems_ToDoItemId",
                table: "UserTableItem");

            migrationBuilder.RenameColumn(
                name: "ToDoItemId",
                table: "UserTableItem",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ToDoTableId",
                table: "UserTable",
                newName: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTable_ToDoTables_TableId",
                table: "UserTable",
                column: "TableId",
                principalTable: "ToDoTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTableItem_ToDoItems_ItemId",
                table: "UserTableItem",
                column: "ItemId",
                principalTable: "ToDoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_ToDoTables_TableId",
                table: "UserTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTableItem_ToDoItems_ItemId",
                table: "UserTableItem");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IsCompleated",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "UserTableItem",
                newName: "ToDoItemId");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "UserTable",
                newName: "ToDoTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTable_ToDoTables_ToDoTableId",
                table: "UserTable",
                column: "ToDoTableId",
                principalTable: "ToDoTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTableItem_ToDoItems_ToDoItemId",
                table: "UserTableItem",
                column: "ToDoItemId",
                principalTable: "ToDoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
