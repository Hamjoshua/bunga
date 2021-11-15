using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class DelProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id_профиль = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_роль = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Арендатор = table.Column<bool>(type: "bit", nullable: false),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Отчество = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Пароль = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Почта = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Реквизиты = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Фамилия = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id_профиль);
                });
        }
    }
}
