using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class ChBungalo_AvgRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Количество_челоек",
                table: "Bungalo",
                newName: "Количество_человек");

            migrationBuilder.AddColumn<float>(
                name: "Оценка",
                table: "Bungalo",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Оценка",
                table: "Bungalo");

            migrationBuilder.RenameColumn(
                name: "Количество_человек",
                table: "Bungalo",
                newName: "Количество_челоек");
        }
    }
}
