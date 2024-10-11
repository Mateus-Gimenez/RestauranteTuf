using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Identity
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime Inclusao { get; set; } = DateTime.Now;
        public DateTime Alteracao { get; set; } = DateTime.Now;
    }
}
