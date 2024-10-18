using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IRepository<T> where T : GuardFoodCommon
    {
        IEnumerable<T> BuscarTodos();

        IEnumerable<T> BuscarTodos(string[] includes);

        IEnumerable<T> BuscarTodosPorRestauranteId(Guid restauranteId);

        IEnumerable<T> BuscarTodosPorRestauranteId(Guid restauranteId, string[] includes);

        void Inserir(T classe);

        void Editar(T classe);

        T BuscarPorId(Guid id);

        RetornoViewModel Deletar(Guid id);

        RetornoViewModel InserirEditar(T entidade);
    }
}
