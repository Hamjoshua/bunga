using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class AddBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id_бронь = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Дата_начала = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Дата_конца = table.Column<DateTime>(type: "datetime2", nullable: false),
                    АрендаторId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id_бронь);
                    table.ForeignKey(
                        name: "FK_Booking_AspNetUsers_АрендаторId",
                        column: x => x.АрендаторId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_АрендаторId",
                table: "Booking",
                column: "АрендаторId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
