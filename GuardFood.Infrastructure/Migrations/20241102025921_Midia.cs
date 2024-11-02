using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuardFood.Core.Migrations
{
    public partial class Midia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MidiaId",
                table: "Restaurante",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MidiaId",
                table: "Produto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Midia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arquivo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_MidiaId",
                table: "Restaurante",
                column: "MidiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_MidiaId",
                table: "Produto",
                column: "MidiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Midia_MidiaId",
                table: "Produto",
                column: "MidiaId",
                principalTable: "Midia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurante_Midia_MidiaId",
                table: "Restaurante",
                column: "MidiaId",
                principalTable: "Midia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Midia_MidiaId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurante_Midia_MidiaId",
                table: "Restaurante");

            migrationBuilder.DropTable(
                name: "Midia");

            migrationBuilder.DropIndex(
                name: "IX_Restaurante_MidiaId",
                table: "Restaurante");

            migrationBuilder.DropIndex(
                name: "IX_Produto_MidiaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "MidiaId",
                table: "Restaurante");

            migrationBuilder.DropColumn(
                name: "MidiaId",
                table: "Produto");
        }
    }
}
