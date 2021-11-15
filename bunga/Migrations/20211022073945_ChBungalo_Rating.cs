using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class ChBungalo_Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Название",
                table: "Bungalo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Описание",
                table: "Bungalo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id_рейтинг = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    БунгалоId_бунгало = table.Column<int>(type: "int", nullable: true),
                    ПокупательId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id_рейтинг);
                    table.ForeignKey(
                        name: "FK_Rating_AspNetUsers_ПокупательId",
                        column: x => x.ПокупательId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_Bungalo_БунгалоId_бунгало",
                        column: x => x.БунгалоId_бунгало,
                        principalTable: "Bungalo",
                        principalColumn: "Id_бунгало",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_БунгалоId_бунгало",
                table: "Rating",
                column: "БунгалоId_бунгало");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ПокупательId",
                table: "Rating",
                column: "ПокупательId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropColumn(
                name: "Название",
                table: "Bungalo");

            migrationBuilder.DropColumn(
                name: "Описание",
                table: "Bungalo");
        }
    }
}
