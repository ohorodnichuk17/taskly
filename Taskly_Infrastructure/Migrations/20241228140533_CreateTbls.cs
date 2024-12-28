using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ToDoItemEntityId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeRangeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRangeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTables", x => x.Id);
                });

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
                name: "BoardTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    BoardEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardTemplates_Boards_BoardEntityId",
                        column: x => x.BoardEntityId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    BoardEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardLists_Boards_BoardEntityId",
                        column: x => x.BoardEntityId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: true),
                    ToDoTableId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItems_TimeRangeEntity_Id",
                        column: x => x.Id,
                        principalTable: "TimeRangeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoTables_ToDoTableId",
                        column: x => x.ToDoTableId,
                        principalTable: "ToDoTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AttachmentUrl = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CardListEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardLists_CardListEntityId",
                        column: x => x.CardListEntityId,
                        principalTable: "CardLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_TimeRangeEntity_Id",
                        column: x => x.Id,
                        principalTable: "TimeRangeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Cards_CardEntityId",
                        column: x => x.CardEntityId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ToDoItemEntityId",
                table: "AspNetUsers",
                column: "ToDoItemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardEntityUserEntity_UserEntityId",
                table: "BoardEntityUserEntity",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardTemplates_BoardEntityId",
                table: "BoardTemplates",
                column: "BoardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardLists_BoardEntityId",
                table: "CardLists",
                column: "BoardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardListEntityId",
                table: "Cards",
                column: "CardListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CardEntityId",
                table: "Comments",
                column: "CardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoTableId",
                table: "ToDoItems",
                column: "ToDoTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTableEntityUserEntity_UserEntityId",
                table: "ToDoTableEntityUserEntity",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Avatars_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ToDoItems_ToDoItemEntityId",
                table: "AspNetUsers",
                column: "ToDoItemEntityId",
                principalTable: "ToDoItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Avatars_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ToDoItems_ToDoItemEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "BoardEntityUserEntity");

            migrationBuilder.DropTable(
                name: "BoardTemplates");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoTableEntityUserEntity");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "ToDoTables");

            migrationBuilder.DropTable(
                name: "CardLists");

            migrationBuilder.DropTable(
                name: "TimeRangeEntity");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ToDoItemEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ToDoItemEntityId",
                table: "AspNetUsers");
        }
    }
}
