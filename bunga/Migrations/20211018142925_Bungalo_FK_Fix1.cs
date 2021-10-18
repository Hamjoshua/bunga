using Microsoft.EntityFrameworkCore.Migrations;

namespace bunga.Migrations
{
    public partial class Bungalo_FK_Fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id",
                table: "Bungalo");

            migrationBuilder.DropIndex(
                name: "IX_Bungalo_Id",
                table: "Bungalo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bungalo");

            migrationBuilder.AlterColumn<string>(
                name: "Id_сдающий",
                table: "Bungalo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bungalo_Id_сдающий",
                table: "Bungalo",
                column: "Id_сдающий");

            migrationBuilder.AddForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id_сдающий",
                table: "Bungalo",
                column: "Id_сдающий",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id_сдающий",
                table: "Bungalo");

            migrationBuilder.DropIndex(
                name: "IX_Bungalo_Id_сдающий",
                table: "Bungalo");

            migrationBuilder.AlterColumn<string>(
                name: "Id_сдающий",
                table: "Bungalo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Bungalo",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bungalo_Id",
                table: "Bungalo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bungalo_AspNetUsers_Id",
                table: "Bungalo",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
