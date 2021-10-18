using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class Bungalo_FK_Fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id_сдающий",
                table: "Bungalo");

            migrationBuilder.RenameColumn(
                name: "Id_сдающий",
                table: "Bungalo",
                newName: "СдающийId");

            migrationBuilder.RenameIndex(
                name: "IX_Bungalo_Id_сдающий",
                table: "Bungalo",
                newName: "IX_Bungalo_СдающийId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bungalo_AspNetUsers_СдающийId",
                table: "Bungalo",
                column: "СдающийId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bungalo_AspNetUsers_СдающийId",
                table: "Bungalo");

            migrationBuilder.RenameColumn(
                name: "СдающийId",
                table: "Bungalo",
                newName: "Id_сдающий");

            migrationBuilder.RenameIndex(
                name: "IX_Bungalo_СдающийId",
                table: "Bungalo",
                newName: "IX_Bungalo_Id_сдающий");

            migrationBuilder.AddForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id_сдающий",
                table: "Bungalo",
                column: "Id_сдающий",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
