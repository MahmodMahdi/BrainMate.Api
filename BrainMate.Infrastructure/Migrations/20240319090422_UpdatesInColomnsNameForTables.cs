using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesInColomnsNameForTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "medicines",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "foods",
                newName: "NameEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "medicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "medicines");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "foods");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "medicines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "foods",
                newName: "Name");
        }
    }
}
