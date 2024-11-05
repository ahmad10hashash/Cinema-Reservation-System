using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.Migrations
{
    /// <inheritdoc />
    public partial class ninth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HallId",
                table: "SeatsMaps");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "SeatsMaps");

            migrationBuilder.AddColumn<string>(
                name: "HallName",
                table: "SeatsMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "SeatsMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hallType",
                table: "SeatsMaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HallName",
                table: "SeatsMaps");

            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "SeatsMaps");

            migrationBuilder.DropColumn(
                name: "hallType",
                table: "SeatsMaps");

            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "SeatsMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "SeatsMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
