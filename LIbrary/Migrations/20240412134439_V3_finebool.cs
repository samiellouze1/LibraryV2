using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIbrary.Migrations
{
    /// <inheritdoc />
    public partial class V3_finebool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "FineStatus");

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "FineStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "FineStatus");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "FineStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
