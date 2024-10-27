using GuardFood.Core.Entities;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetByCategoriaId(Guid categoriaId);
    }
}
