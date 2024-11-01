using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("PedidoProduto")]
    public class PedidoProduto : GuardFoodCommon
    {
        [ForeignKey("Pedido")]
        public Guid PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }

        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; } = 0;

        public decimal ValorUnitario { get; set; }

        public string NomeProduto {  get; set; }

        public string Observacao {  get; set; }

        [NotMapped]
        public decimal Valor
        {
            get
            {
                return ValorUnitario * Quantidade;
            }
        }

        [NotMapped]
        public string ValorUnitarioFormatado
        {
            get
            {
                return ValorUnitario.ToString("C", new CultureInfo("pt-BR"));
            }
        }

        [NotMapped]
        public string ValorFormatado
        {
            get
            {
                return Valor.ToString("C", new CultureInfo("pt-BR"));
            }
        }
    }
}
