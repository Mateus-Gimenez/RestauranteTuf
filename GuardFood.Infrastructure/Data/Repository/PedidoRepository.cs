using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;

namespace GuardFood.Core.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(GFContext context) : base(context)
        {
        }
    }
}