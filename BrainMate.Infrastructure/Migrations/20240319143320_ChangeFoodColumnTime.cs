using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainMate.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class ChangeFoodColumnTime : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Date",
				table: "foods");

			migrationBuilder.AddColumn<TimeOnly>(
				name: "Time",
				table: "foods",
				type: "time",
				nullable: true,
				defaultValue: new TimeOnly(0, 0, 0));
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
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
