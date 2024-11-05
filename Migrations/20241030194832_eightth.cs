using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.Migrations
{
    /// <inheritdoc />
    public partial class eightth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImaxDateAndTime",
                table: "ShowsTimes");

            migrationBuilder.RenameColumn(
                name: "StandardDateAndTime",
                table: "ShowsTimes",
                newName: "DateAndTime");

            migrationBuilder.RenameColumn(
                name: "HallType",
                table: "ShowsTimes",
                newName: "HallName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HallName",
                table: "ShowsTimes",
                newName: "HallType");

            migrationBuilder.RenameColumn(
                name: "DateAndTime",
                table: "ShowsTimes",
                newName: "StandardDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ImaxDateAndTime",
                table: "ShowsTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
