using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Pedido")]
    public class Pedido : GuardFoodCommon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        public string NomeCliente { get; set; }

        [ForeignKey("Mesa")]
        public Guid MesaId { get; set; }
        public Mesa Mesa { get; set; }

        [NotMapped]
        public string CodigoFormatado
        {
            get
            {
                return Codigo.ToString("00000");
            }
        } 
    }
}