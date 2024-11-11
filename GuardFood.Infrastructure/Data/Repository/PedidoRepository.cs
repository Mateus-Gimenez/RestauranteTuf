using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuardFood.Core.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(GFContext context) : base(context) { }

        public IEnumerable<Pedido> GetByTelefone(Guid restauranteId, string telefone)
        {
            return _context.Pedidos.Include(i => i.Mesa).Where(w => w.Ativo && w.RestauranteId == restauranteId && w.Telefone == telefone).OrderByDescending(o => o.Alteracao).ToList();
        }

        public RetornoViewModel InserePedido(Pedido pedido, List<PedidoProduto> pedidoProdutos) 
        {
            try
            {
                _context.Pedidos.Add(pedido);
                _context.PedidoProdutos.AddRange(pedidoProdutos);

                _context.SaveChanges();

                return new RetornoViewModel() { Sucesso = true,  Mensagem = "Pedido realizado com sucesso" };
            }
            catch (Exception e)
            {
                return new RetornoViewModel() { Sucesso = false, Mensagem = e.Message };
            }
        }

    }
}