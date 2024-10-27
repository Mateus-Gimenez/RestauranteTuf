using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("ProdutoCategoria")]
    public class ProdutoCategoria : GuardFoodCommon
    {
        public int Ordem { get; set; }
        public string Nome { get; set; }

        [NotMapped]
        public List<Produto> Produtos { get; set; }
    }
}
