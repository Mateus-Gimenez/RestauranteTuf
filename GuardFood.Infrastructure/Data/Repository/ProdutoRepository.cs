using GuardFood.Core.Entities;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;

namespace GuardFood.Core.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(GFContext context) : base(context) { }

        public IEnumerable<Produto> GetByCategoriaId(Guid categoriaId)
        {
            return _context.Produtos.Where(w => w.ProdutoCategoriaId == categoriaId && w.Ativo).ToList();
        }
    }
}