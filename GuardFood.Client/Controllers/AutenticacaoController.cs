using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuardFood.Client.Controllers
{
    [AllowAnonymous]
    public class AutenticacaoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRestauranteRepository _restauranteRepository;

        public AutenticacaoController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IUsuarioRepository usuarioRepository, IRestauranteRepository restauranteRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioRepository = usuarioRepository;
            _restauranteRepository = restauranteRepository;
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (!_usuarioRepository.VerificaUsuarios())
            {
                var restaurante = new Restaurante()
                {
                    Nome = "Restaurante Master",
                    Descricao = "Master",
                    Email = "master@gmail.com",
                };

                _restauranteRepository.InsertOrReplace(restaurante);

                var usuarioMaster = new Usuario()
                {
                    UserName = "master",
                    Nome = "Master",
                    Email = "master@gmail.com",
                    PhoneNumber = "1234567890",
                    RestauranteId = restaurante.Id,
                };
                _userManager.CreateAsync(usuarioMaster, "Master@2024");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public RetornoViewModel RealizaLogin(AutenticacaoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userManager.FindByNameAsync(model.Usuario).Result;

                    // Verifica se o usuário existe
                    if (user == null || !user.Ativo) throw new Exception("Usuário ou Senha Inválidos");

                    var result = _signInManager.PasswordSignInAsync(model.Usuario, model.Senha, model.Lembrar, lockoutOnFailure: false).Result;

                    if (result.Succeeded)
                    {
                        return new RetornoViewModel
                        {
                            Sucesso = true,
                        };
                    }
                    else
                    {
                        throw new Exception("Usuário ou Senha Inválidos");
                    }
                }

                throw new Exception("Falha no login");
            }
            catch (Exception e)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = e.Message
                };
            }
        }

        [HttpPost]
        public RetornoViewModel TrocaSenha(SenhaViewModel model)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;

                // Verifica se o usuário existe
                if (user == null || !user.Ativo) throw new Exception("Usuário não encontrado");
                if (model.SenhaNova != model.RepetirSenhaNova) throw new Exception("Senhas novas estão diferentes");

                var retorno = _userManager.ChangePasswordAsync(user, model.SenhaAtual, model.SenhaNova).Result;
                if (retorno.Succeeded)
                {
                    return new RetornoViewModel
                    {
                         Sucesso = true,                       
                        Mensagem = "Senha alterada com sucesso"
                    };
                }
                else
                {
                    throw new Exception($"Ocorreram alguns erros ao alterar a senha: {string.Join(", ", retorno.Errors.Select(s => s.Description))}");
                }

            }
            catch (Exception e)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = e.Message
                };
            }
        }

        [HttpGet]
        [Route("/Sair")]
        public IActionResult Sair()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
