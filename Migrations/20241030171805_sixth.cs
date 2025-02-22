﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImaxShowTimes",
                table: "ShowsTimes");

            migrationBuilder.RenameColumn(
                name: "StandardShowTimes",
                table: "ShowsTimes",
                newName: "HallType");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ShowsTimes",
                newName: "StandardDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ImaxDateAndTime",
                table: "ShowsTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImaxDateAndTime",
                table: "ShowsTimes");

            migrationBuilder.RenameColumn(
                name: "StandardDateAndTime",
                table: "ShowsTimes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "HallType",
                table: "ShowsTimes",
                newName: "StandardShowTimes");

            migrationBuilder.AddColumn<string>(
                name: "ImaxShowTimes",
                table: "ShowsTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
