using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        RetornoViewModel CancelarPedido(Pedido pedido);
    }
}
