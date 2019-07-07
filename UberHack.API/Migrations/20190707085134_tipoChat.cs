using Microsoft.EntityFrameworkCore.Migrations;

namespace UberHack.API.Migrations
{
    public partial class tipoChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "FaculdadeId",
                table: "Usuario",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TipoChat",
                table: "Chat",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario",
                column: "FaculdadeId",
                principalTable: "Faculdade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TipoChat",
                table: "Chat");

            migrationBuilder.AlterColumn<int>(
                name: "FaculdadeId",
                table: "Usuario",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario",
                column: "FaculdadeId",
                principalTable: "Faculdade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
