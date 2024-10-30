using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuardFood.Core.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : GuardFoodCommon
    {
        protected readonly GFContext _context;

        public Repository(GFContext context)
        {
            _context = context;
        }

        public T BuscarPorId(Guid id)
        {
            try
            {
                return _context.Set<T>().FirstOrDefault(f => f.Id == id && f.Ativo);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public RetornoViewModel Deletar(Guid id)
        {
            try
            {
                var entidade = _context.Set<T>().Find(id) ?? throw new Exception("Registro não encontrado");

                entidade.Alteracao = DateTime.Now;
                entidade.Ativo = false;
                _context.Entry(entidade).State = EntityState.Modified;
                _context.SaveChanges();

                return new RetornoViewModel { Sucesso = true, Mensagem = "Registro removido com sucesso" };
            }
            catch (Exception e)
            {
                return new RetornoViewModel { Sucesso = false, Mensagem = "Erro ao remover: " + e.Message };
            }
        }

        public void Editar(T classe)
        {
            _context.Entry(classe).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<T> BuscarTodos()
        {
            return _context.Set<T>().Where(w => w.Ativo).ToList();
        }

        public IEnumerable<T> BuscarTodos(string[] includes)
        {
            var dados = _context.Set<T>().Where(t => t.Ativo);

            IQueryable<T> dadosCompletos = null;

            foreach (var include in includes)
            {
                dadosCompletos = dados.Include(include);
            }

            return dadosCompletos.ToList();
        }

        public IEnumerable<T> BuscarTodosPorRestauranteId(Guid restauranteId)
        {
            return _context.Set<T>().Where(w => w.Ativo && w.RestauranteId == restauranteId).ToList();
        }

        public IEnumerable<T> BuscarTodosPorRestauranteId(Guid restauranteId, string[] includes)
        {
            var dados = _context.Set<T>().Where(t => t.Ativo && t.RestauranteId == restauranteId);


            IQueryable<T> dadosCompletos = null;

            foreach (var include in includes)
            {
                dadosCompletos = dados.Include(include);
            }

            return dadosCompletos.ToList();
        }

        public void Inserir(T entidade)
        {
            _context.Set<T>().Add(entidade);
            _context.SaveChanges();
        }

        public RetornoViewModel InserirEditar(T entidade)
        {
            try
            {
                var possui = _context.Set<T>().Any(a => a.Id == entidade.Id);
                
                entidade.Alteracao = DateTime.Now;
                entidade.Ativo = true;

                if (possui)
                {
                    Editar(entidade);
                }
                else
                {
                    entidade.Inclusao = DateTime.Now;
                    Inserir(entidade);
                }

                return new RetornoViewModel()
                {
                    Sucesso = true,
                    Mensagem = $"Registro {(possui ? "editado" : "inserido")} com sucesso"
                };
            }
            catch (Exception e) 
            {
                return new RetornoViewModel()
                {
                    Sucesso = false,
                    Mensagem = "Erro: " + e.Message,
                };
            }
        }
    }
}
