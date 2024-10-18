using GuardFood.Core.Entities;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;

namespace GuardFood.Core.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly GFContext _context;
        public ProdutoRepository(GFContext context) : base(context)
        {
            _context = context;
        }
    }
}