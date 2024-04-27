using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaregiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaregiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: true),
                    StartAt = table.Column<DateOnly>(type: "date", nullable: true),
                    EndAt = table.Column<DateOnly>(type: "date", nullable: true),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaregiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "relatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationShipDegree = table.Column<int>(type: "int", nullable: true),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaregiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relatives", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "foods");

            migrationBuilder.DropTable(
                name: "medicines");

            migrationBuilder.DropTable(
                name: "relatives");
        }
    }
}
