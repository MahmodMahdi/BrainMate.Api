using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFoodColomnDateAndAddImgColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "foods");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "foods",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "foods");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "foods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
