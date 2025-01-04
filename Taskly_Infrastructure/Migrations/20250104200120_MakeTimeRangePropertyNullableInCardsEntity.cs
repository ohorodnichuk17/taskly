using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeTimeRangePropertyNullableInCardsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards",
                column: "Id",
                principalTable: "TimeRangeEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_TimeRangeEntity_Id",
                table: "Cards",
                column: "Id",
                principalTable: "TimeRangeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
