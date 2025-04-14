using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNameFieldToTableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ToDoTables",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ToDoTables");
        }
    }
}
