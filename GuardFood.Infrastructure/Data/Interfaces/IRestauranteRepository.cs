using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IRestauranteRepository
    {
        Restaurante GetById(Guid restauranteId);
        void InsertOrReplace(Restaurante restaurante);
        void Delete(Guid Id);
    }
}
