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
            return _context.Produtos.Where(w => w.ProdutoCategoriaId == categoriaId && w.Ativo).OrderBy(o => o.Ordem).ToList();
        }

        public void Reordenar(Guid categoriaId)
        {
            var produtos = _context.Produtos.Where(w => w.ProdutoCategoriaId == categoriaId && w.Ativo).OrderBy(o => o.Ordem).ToList();

            var ordem = 1;
            foreach(var p in produtos)
            {
                p.Ordem = ordem;
                ordem ++;
            }

            _context.Produtos.UpdateRange(produtos);
            _context.SaveChanges();
        }
    }
}