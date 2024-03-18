using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientTableColomns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "patients");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "patients",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "patients");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
