using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class FixBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "БунгалоId_бунгало",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_БунгалоId_бунгало",
                table: "Booking",
                column: "БунгалоId_бунгало");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Bungalo_БунгалоId_бунгало",
                table: "Booking",
                column: "БунгалоId_бунгало",
                principalTable: "Bungalo",
                principalColumn: "Id_бунгало",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Bungalo_БунгалоId_бунгало",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_БунгалоId_бунгало",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "БунгалоId_бунгало",
                table: "Booking");
        }
    }
}
