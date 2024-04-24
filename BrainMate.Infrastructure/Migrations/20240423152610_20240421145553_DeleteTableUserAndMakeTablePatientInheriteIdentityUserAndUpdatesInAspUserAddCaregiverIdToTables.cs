using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20240421145553_DeleteTableUserAndMakeTablePatientInheriteIdentityUserAndUpdatesInAspUserAddCaregiverIdToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_patients_PatientId",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_foods_patients_PatientId",
                table: "foods");

            migrationBuilder.DropForeignKey(
                name: "FK_medicines_patients_PatientId",
                table: "medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_relatives_patients_PatientId",
                table: "relatives");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "userRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "RelationShipDegree",
                table: "relatives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "relatives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CaregiverEmail",
                table: "relatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientEmail",
                table: "relatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaregiverEmail",
                table: "medicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientEmail",
                table: "medicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaregiverEmail",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientEmail",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaregiverEmail",
                table: "events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientEmail",
                table: "events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "patientRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patientRefreshTokens_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patientRefreshTokens_PatientId",
                table: "patientRefreshTokens",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_AspNetUsers_PatientId",
                table: "events",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_foods_AspNetUsers_PatientId",
                table: "foods",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_medicines_AspNetUsers_PatientId",
                table: "medicines",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_relatives_AspNetUsers_PatientId",
                table: "relatives",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_AspNetUsers_PatientId",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_foods_AspNetUsers_PatientId",
                table: "foods");

            migrationBuilder.DropForeignKey(
                name: "FK_medicines_AspNetUsers_PatientId",
                table: "medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_relatives_AspNetUsers_PatientId",
                table: "relatives");

            migrationBuilder.DropTable(
                name: "patientRefreshTokens");

            migrationBuilder.DropColumn(
                name: "CaregiverEmail",
                table: "relatives");

            migrationBuilder.DropColumn(
                name: "PatientEmail",
                table: "relatives");

            migrationBuilder.DropColumn(
                name: "CaregiverEmail",
                table: "medicines");

            migrationBuilder.DropColumn(
                name: "PatientEmail",
                table: "medicines");

            migrationBuilder.DropColumn(
                name: "CaregiverEmail",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "PatientEmail",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "CaregiverEmail",
                table: "events");

            migrationBuilder.DropColumn(
                name: "PatientEmail",
                table: "events");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Job",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "RelationShipDegree",
                table: "relatives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "relatives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userRefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userRefreshTokens_UserId",
                table: "userRefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_patients_PatientId",
                table: "events",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_foods_patients_PatientId",
                table: "foods",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_medicines_patients_PatientId",
                table: "medicines",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_relatives_patients_PatientId",
                table: "relatives",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id");
        }
    }
}
