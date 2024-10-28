using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Repository
{
    public class ProdutoCategoriaRepository : Repository<ProdutoCategoria>, IProdutoCategoriaRepository
    {
        public ProdutoCategoriaRepository(GFContext context) : base(context) { }

        public void Reordenar(Guid restauranteId)
        {
            var categorias = _context.ProdutoCategorias.Where(w => w.RestauranteId == restauranteId && w.Ativo).OrderBy(o => o.Ordem).ToList();

            var ordem = 1;
            foreach (var c in categorias)
            {
                c.Ordem = ordem;
                ordem++;
            }

            _context.ProdutoCategorias.UpdateRange(categorias);
            _context.SaveChanges();
        }
    }
}
