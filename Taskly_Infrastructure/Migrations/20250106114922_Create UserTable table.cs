using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserTabletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoTableEntityUserEntity");

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    ToDoTableId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => new { x.ToDoTableId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTable_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTable_ToDoTables_ToDoTableId",
                        column: x => x.ToDoTableId,
                        principalTable: "ToDoTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_UserId",
                table: "UserTable",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.CreateTable(
                name: "ToDoTableEntityUserEntity",
                columns: table => new
                {
                    ToDoTablesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTableEntityUserEntity", x => new { x.ToDoTablesId, x.UserEntityId });
                    table.ForeignKey(
                        name: "FK_ToDoTableEntityUserEntity_AspNetUsers_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoTableEntityUserEntity_ToDoTables_ToDoTablesId",
                        column: x => x.ToDoTablesId,
                        principalTable: "ToDoTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTableEntityUserEntity_UserEntityId",
                table: "ToDoTableEntityUserEntity",
                column: "UserEntityId");
        }
    }
}
