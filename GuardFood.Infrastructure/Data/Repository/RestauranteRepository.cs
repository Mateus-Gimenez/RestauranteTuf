using GuardFood.Core.Entities;
using GuardFood.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuardFood.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace GuardFood.Core.Data.Repository
{
    public class RestauranteRepository : Repository<Restaurante>, IRestauranteRepository
    {
        public RestauranteRepository(GFContext context) : base(context)
        {
            
        }
    }
}
