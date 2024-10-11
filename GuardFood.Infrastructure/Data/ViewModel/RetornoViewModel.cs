using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.ViewModel
{
    public class RetornoViewModel
    {
        public bool Sucesso { get; set; } = true;
        public string? Mensagem { get; set; }
        public int? Codigo { get; set; }
    }
}
