using GuardFood.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(string id);
        void InsertOrReplace(Usuario usuario);
        void Delete(string id);
        bool VerificaUsuarios();
    }
}
