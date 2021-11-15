using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class ChRating_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Оценка",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Оценка",
                table: "Rating");
        }
    }
}
