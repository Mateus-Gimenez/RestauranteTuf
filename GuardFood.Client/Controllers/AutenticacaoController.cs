﻿using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuardFood.Client.Controllers
{
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
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl = null)
        {
            if(_userManager.GetUserAsync(User).Result != null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

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

        [HttpGet]
        [AllowAnonymous]
        [Route("/Cadastrar")]
        public IActionResult Cadastrar()
        {
            if (_userManager.GetUserAsync(User).Result != null)
            {
                return RedirectToAction("Dashboard", "Home");
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
        public RetornoViewModel PostRestauranteUsuario(string values)
        {
            try
            {
                var model = new FormCadastroViewModel();
                JsonConvert.PopulateObject(values, model);

                var restaurate = new Restaurante()
                {
                    Nome = model.Restaurante.Nome,
                    Telefone = model.Restaurante.Telefone,
                    Endereco = model.Restaurante.Endereco
                };

                var usuario = new Usuario()
                {
                    Nome = model.Usuario.Nome,
                    UserName = model.Usuario.Email,
                    Email = model.Usuario.Email,
                    RestauranteId = restaurate.Id
                };

                _restauranteRepository.InsertOrReplace(restaurate);
                var retorno = _userManager.CreateAsync(usuario, model.Usuario.Senha).Result;
                if (retorno.Errors.Any()) throw new Exception(string.Join(", ", retorno.Errors));


                return new RetornoViewModel { Sucesso = true, Mensagem = "Cadastro realizado com sucesso" };
            }
            catch(Exception e)
            {
                return new RetornoViewModel { Sucesso = false, Mensagem = e.Message };
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
        [AllowAnonymous]
        public IActionResult Sair()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
