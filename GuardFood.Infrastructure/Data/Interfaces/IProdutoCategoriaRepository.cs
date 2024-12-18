﻿using GuardFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IProdutoCategoriaRepository : IRepository<ProdutoCategoria>
    {
        void Reordenar(Guid restauranteId);
    }
}
