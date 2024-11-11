using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        IEnumerable<Pedido> GetByTelefone(Guid restauranteId, string telefone);
        RetornoViewModel InserePedido(Pedido pedido, List<PedidoProduto> pedidoProdutos);
    }
}
