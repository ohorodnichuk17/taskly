using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskly_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTimeRangeWithCreatedAtColumnInFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_TimeRanges_TimeRangeId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_TimeRangeId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "TimeRangeId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedbacks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Feedbacks");

            migrationBuilder.AddColumn<Guid>(
                name: "TimeRangeId",
                table: "Feedbacks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TimeRangeId",
                table: "Feedbacks",
                column: "TimeRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_TimeRanges_TimeRangeId",
                table: "Feedbacks",
                column: "TimeRangeId",
                principalTable: "TimeRanges",
                principalColumn: "Id");
        }
    }
}
