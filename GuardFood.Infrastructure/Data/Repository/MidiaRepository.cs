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
    public class MidiaRepository : IMidiaRepository
    {
        protected readonly GFContext _dbContext;

        public MidiaRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Midia GetById(Guid Id)
        {
            return _dbContext.Midias.SingleOrDefault(t => t.Id == Id);
        }

        public void Delete(Guid Id)
        {
            var Entity = _dbContext.Midias.SingleOrDefault(sd => sd.Id == Id) ?? throw new Exception("Registro não encontrado");

            Entity.Ativo = false;
            Entity.Alteracao = DateTime.Now;

            _dbContext.Entry(Entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void InsertOrReplace(Midia objeto)
        {
            try
            {
                var Entity = _dbContext.Midias.AsNoTracking().SingleOrDefault(e => e.Id == objeto.Id);

                objeto.Ativo = true;
                objeto.Alteracao = DateTime.Now;

                if (Entity == null)
                {
                    objeto.Inclusao = DateTime.Now;
                    _dbContext.Midias.Add(objeto);
                }
                //se encoutrou
                else
                {
                    _dbContext.Entry(objeto).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
