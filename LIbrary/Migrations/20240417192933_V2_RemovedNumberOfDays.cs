using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIbrary.Migrations
{
    /// <inheritdoc />
    public partial class V2_RemovedNumberOfDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Fine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Fine",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
