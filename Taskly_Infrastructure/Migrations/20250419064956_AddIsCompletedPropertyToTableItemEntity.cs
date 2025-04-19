using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompletedPropertyToTableItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ToDoItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ToDoItems");
        }
    }
}
