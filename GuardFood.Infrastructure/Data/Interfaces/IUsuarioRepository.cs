﻿using GuardFood.Core.Data.ViewModel;
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
        IEnumerable<Usuario> GetFuncionariosByRestauranteId(Guid restauranteId);
        IEnumerable<Usuario> GetMasterByRestauranteId(Guid restauranteId);
        Usuario GetById(string id);
        RetornoViewModel InsertOrReplace(Usuario usuario);
        void Delete(string id);
        bool VerificaUsuarios();
    }
}
