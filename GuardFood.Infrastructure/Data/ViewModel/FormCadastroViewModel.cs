using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Data.ViewModel
{
    public class FormCadastroViewModel
    {
        public FormCadastroUsuarioViewModel Usuario { get; set; } = new FormCadastroUsuarioViewModel();
        public FormCadastroRestauranteViewModel Restaurante { get; set; } = new FormCadastroRestauranteViewModel();
    }

    public class FormCadastroUsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
    }

    public class FormCadastroRestauranteViewModel
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
