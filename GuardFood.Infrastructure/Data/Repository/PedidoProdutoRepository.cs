using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Repository
{
    public class PedidoProdutoRepository : Repository<PedidoProduto>, IPedidoProdutoRepository
    {
        public PedidoProdutoRepository(GFContext context) : base(context) { }

        public IEnumerable<PedidoProduto> GetByPedidoId(Guid pedidoId)
        {
            return _context.PedidoProdutos.Include(i => i.Produto).Where(w => w.Ativo && w.PedidoId == pedidoId).ToList();
        }

        public IEnumerable<PedidoProduto> GetDashboard(Guid restauranteId)
        {
            return _context.PedidoProdutos.Include(i => i.Produto).Include(i => i.Pedido).Where(w => w.Ativo && w.RestauranteId == restauranteId).ToList();
        }
    }
}
