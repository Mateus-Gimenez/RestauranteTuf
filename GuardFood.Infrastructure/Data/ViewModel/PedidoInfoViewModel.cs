using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.ViewModel
{
    public class PedidoInfoViewModel
    {
        public PedidoViewModel Pedido { get; set; }
        public List<PedidoProdutoViewModel> PedidoProdutos { get; set; }
    }

    public class PedidoViewModel
    {
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
    }

    public class PedidoProdutoViewModel
    {
        public int Quantidade { get; set; }
        public Guid ProdutoId { get; set; }
        public string Observacao { get; set; }
    }

}
