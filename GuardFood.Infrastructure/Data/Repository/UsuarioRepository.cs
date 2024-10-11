using GuardFood.Core.Context;
using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly GFContext _dbContext;

        public UsuarioRepository(GFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _dbContext.Usuarios.Where(u => u.Ativo).AsEnumerable();
        }

        public Usuario GetById(string id)
        {
            return _dbContext.Usuarios.SingleOrDefault(t => t.Id == id);
        }

        public void Delete(string Id)
        {
            var Entity = _dbContext.Usuarios.SingleOrDefault(sd => sd.Id == Id) ?? throw new Exception("Registro não encontrado");

            Entity.Ativo = false;
            Entity.Alteracao = DateTime.Now;

            _dbContext.Entry(Entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void InsertOrReplace(Usuario usuario)
        {
            try
            {
                var Entity = _dbContext.Usuarios.AsNoTracking().SingleOrDefault(e => e.Id == usuario.Id);

                usuario.Ativo = true;

                if (Entity == null)
                {
                    usuario.NormalizedUserName = usuario.NormalizedUserName.ToUpper();
                    usuario.UserName = usuario.UserName;
                    usuario.PasswordHash = usuario.PasswordHash;
                    usuario.NormalizedUserName = usuario.UserName;
                    usuario.Alteracao = DateTime.Now;
                    _dbContext.Usuarios.Add(usuario);
                }
                //se encoutrou
                else
                {
                    usuario.NormalizedUserName = usuario.NormalizedUserName.ToUpper();
                    usuario.UserName = usuario.UserName;
                    usuario.NormalizedUserName = usuario.UserName;
                    usuario.PasswordHash = usuario.PasswordHash;
                    usuario.Alteracao = DateTime.Now;
                    _dbContext.Entry(usuario).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool VerificaUsuarios()
        {
            return _dbContext.Usuarios.Any();
        }
    }
}
