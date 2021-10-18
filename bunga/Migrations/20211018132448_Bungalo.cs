using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class Bungalo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bungalo",
                columns: table => new
                {
                    Id_бунгало = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Адрес = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Количество_челоек = table.Column<int>(type: "int", nullable: false),
                    Wi_fi = table.Column<bool>(type: "bit", nullable: false),
                    Id_сдающий = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Стоимость_сутки = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bungalo", x => x.Id_бунгало);
                    table.ForeignKey(
                        name: "FK_Bungalo_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bungalo_Id",
                table: "Bungalo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bungalo");
        }
    }
}
