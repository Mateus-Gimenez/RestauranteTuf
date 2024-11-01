using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;

namespace GuardFood.Core.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(GFContext context) : base(context) { }

        public RetornoViewModel CancelarPedido(Pedido pedido)
        {
            try
            {
                pedido.Status = Pedido.StatusPedido.Cancelado;
                _context.Pedidos.Update(pedido).Property(x => x.Codigo).IsModified = false;
                _context.SaveChanges();

                return new RetornoViewModel() { Sucesso = true };
            }
            catch(Exception e)
            {
                return new RetornoViewModel() { Sucesso = false, Mensagem = e.Message };
            }
        }
    }
}