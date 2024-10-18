using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Restaurante")]
    public class Restaurante
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Ativo { get; set; } = true;
        public DateTime Inclusao { get; set; } = DateTime.Now;
        public DateTime Alteracao { get; set; } = DateTime.Now;

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string CorPrimaria { get; set; }
        public string CorSecundaria { get; set; }
    }
}
