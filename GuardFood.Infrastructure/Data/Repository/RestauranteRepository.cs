using GuardFood.Core.Entities;
using GuardFood.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuardFood.Core.Context;
using Microsoft.EntityFrameworkCore;
using GuardFood.Core.Identity;
using GuardFood.Core.Data.ViewModel;

namespace GuardFood.Core.Data.Repository
{
    public class RestauranteRepository : IRestauranteRepository
    {
        protected readonly GFContext _dbContext;

        public RestauranteRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Restaurante GetById(Guid Id)
        {
            return _dbContext.Restaurantes.SingleOrDefault(t => t.Id == Id);
        }

        public void Delete(Guid Id)
        {
            var Entity = _dbContext.Restaurantes.SingleOrDefault(sd => sd.Id == Id) ?? throw new Exception("Registro não encontrado");

            Entity.Ativo = false;
            Entity.Alteracao = DateTime.Now;

            _dbContext.Entry(Entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public RetornoViewModel InsertOrReplace(Restaurante restaurante)
        {
            try
            {
                var Entity = _dbContext.Restaurantes.AsNoTracking().SingleOrDefault(e => e.Id == restaurante.Id);

                restaurante.Ativo = true;
                restaurante.Alteracao = DateTime.Now;

                if (Entity == null)
                {
                    restaurante.Inclusao = DateTime.Now;
                    _dbContext.Restaurantes.Add(restaurante);
                }
                //se encoutrou
                else
                {
                    _dbContext.Entry(restaurante).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();

                return new RetornoViewModel() { Sucesso = true, Mensagem = $"Configurações editadas com sucesso" };
            }
            catch (Exception e)
            {
                return new RetornoViewModel() { Sucesso = false, Mensagem = e.Message };
            }
        }
    }
}
