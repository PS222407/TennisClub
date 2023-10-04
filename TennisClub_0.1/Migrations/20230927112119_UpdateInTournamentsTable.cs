using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisClub_0._1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInTournamentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tournament",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tournament");
        }
    }
}
