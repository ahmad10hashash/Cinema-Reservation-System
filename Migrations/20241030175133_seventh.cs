using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRS.Migrations
{
    /// <inheritdoc />
    public partial class seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "ShowsTimes");

            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "ShowsTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "ShowsTimes");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "ShowsTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
