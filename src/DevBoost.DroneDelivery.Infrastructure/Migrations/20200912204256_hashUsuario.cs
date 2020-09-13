using Microsoft.EntityFrameworkCore.Migrations;

namespace DevBoost.DroneDelivery.Infrastructure.Migrations
{
    public partial class hashUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_ClienteId",
                table: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Usuario",
                type: "Varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(30)");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ClienteId",
                table: "Usuario",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_ClienteId",
                table: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Usuario",
                type: "Varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ClienteId",
                table: "Usuario",
                column: "ClienteId",
                unique: true);
        }
    }
}
