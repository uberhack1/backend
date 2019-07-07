using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UberHack.API.Migrations
{
    public partial class ChatUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Funcionario_UsuarioId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Bairro_BairroCasaId",
                table: "Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Empresa_EmpresaId",
                table: "Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Faculdade_FaculdadeId",
                table: "Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Funcionario_UsuarioId",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Chat_UsuarioId",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Chat");

            migrationBuilder.RenameTable(
                name: "Funcionario",
                newName: "Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Funcionario_FaculdadeId",
                table: "Usuario",
                newName: "IX_Usuario_FaculdadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Funcionario_EmpresaId",
                table: "Usuario",
                newName: "IX_Usuario_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Funcionario_BairroCasaId",
                table: "Usuario",
                newName: "IX_Usuario_BairroCasaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChatId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUsuarios_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUsuarios_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsuarios_ChatId",
                table: "ChatUsuarios",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsuarios_UsuarioId",
                table: "ChatUsuarios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Usuario_UsuarioId",
                table: "Mensagem",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Bairro_BairroCasaId",
                table: "Usuario",
                column: "BairroCasaId",
                principalTable: "Bairro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Empresa_EmpresaId",
                table: "Usuario",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario",
                column: "FaculdadeId",
                principalTable: "Faculdade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Usuario_UsuarioId",
                table: "Mensagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Bairro_BairroCasaId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Empresa_EmpresaId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Faculdade_FaculdadeId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "ChatUsuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Funcionario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_FaculdadeId",
                table: "Funcionario",
                newName: "IX_Funcionario_FaculdadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_EmpresaId",
                table: "Funcionario",
                newName: "IX_Funcionario_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_BairroCasaId",
                table: "Funcionario",
                newName: "IX_Funcionario_BairroCasaId");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Chat",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UsuarioId",
                table: "Chat",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Funcionario_UsuarioId",
                table: "Chat",
                column: "UsuarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Bairro_BairroCasaId",
                table: "Funcionario",
                column: "BairroCasaId",
                principalTable: "Bairro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Empresa_EmpresaId",
                table: "Funcionario",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Faculdade_FaculdadeId",
                table: "Funcionario",
                column: "FaculdadeId",
                principalTable: "Faculdade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Funcionario_UsuarioId",
                table: "Mensagem",
                column: "UsuarioId",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
