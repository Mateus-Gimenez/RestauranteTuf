using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuardFood.Core.Migrations
{
    public partial class Ordem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "ProdutoCategoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "ProdutoCategoria");

            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Produto");
        }
    }
}
