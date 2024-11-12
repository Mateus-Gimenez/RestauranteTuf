using GuardFood.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public TipoUsuario Tipo { get; set; }

        [ForeignKey("Restaurante")]
        public Guid RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }

        public enum TipoUsuario
        {
            Master = 0,
            Cozinha = 1,
            Salao = 2,
        }
    }
}
