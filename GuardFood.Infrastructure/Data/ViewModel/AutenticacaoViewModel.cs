using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.ViewModel
{
    public class AutenticacaoViewModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool Lembrar { get; set; }
    }
}
