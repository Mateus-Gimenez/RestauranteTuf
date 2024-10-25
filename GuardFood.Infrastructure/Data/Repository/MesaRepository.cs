using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Repository
{
    public class MesaRepository : Repository<Mesa>, IMesaRepository
    {
        public MesaRepository(GFContext context) : base(context) { }
    }
}
