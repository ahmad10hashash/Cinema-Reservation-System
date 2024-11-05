using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "HallsReservations");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Halls");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reservations",
                newName: "DateAndTime");

            migrationBuilder.AddColumn<int>(
                name: "TheSeat",
                table: "HallsReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SeatsMaps",
                columns: table => new
                {
                    HallId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TheSeat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatsMaps");

            migrationBuilder.DropColumn(
                name: "TheSeat",
                table: "HallsReservations");

            migrationBuilder.RenameColumn(
                name: "DateAndTime",
                table: "Reservations",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "time",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seats",
                table: "HallsReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seats",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
