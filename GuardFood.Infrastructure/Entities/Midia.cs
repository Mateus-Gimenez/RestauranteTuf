using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Midia")]
    public class Midia
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Ativo { get; set; } = true;
        public DateTime Inclusao { get; set; } = DateTime.Now;
        public DateTime Alteracao { get; set; } = DateTime.Now;

        public byte[] Arquivo { get; set; }

        public string Nome { get; set; }

        [NotMapped]
        public string Url { get; set; }

        [NotMapped]
        public string Extensao
        {
            get
            {
                return Path.GetExtension(Nome);
            }
        }

        [NotMapped]
        public string NomeSemExtensao
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Nome);
            }
        }
    }
}
