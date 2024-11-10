using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuardFood.Core.Migrations
{
    public partial class PedidoTelefone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Pedido");
        }
    }
}
