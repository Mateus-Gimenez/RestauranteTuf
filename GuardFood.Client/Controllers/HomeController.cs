using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Rotativa.AspNetCore;
using System.Diagnostics;

namespace GuardFood.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRestauranteRepository _restauranteRepository;
        private readonly IMesaRepository _mesaRepository;

        public HomeController(UserManager<Usuario> userManager, IRestauranteRepository restauranteRepository, IMesaRepository mesaRepository)
        {
            _userManager = userManager;
            _restauranteRepository = restauranteRepository;
            _mesaRepository = mesaRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("~/Views/Home/IndexLayout.cshtml");
        }

        public IActionResult Dashboard()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View();
        }

        [Route("/Usuario")]
        public IActionResult Usuario()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View("~/Views/Usuario/Index.cshtml");
        }

        [Route("/Restaurante/Configuracoes")]
        public IActionResult RestauranteConfiguracoes()
        {
            var restaurante = _restauranteRepository.GetById(_userManager.GetUserAsync(User).Result.RestauranteId);
            return View("~/Views/Restaurante/Configuracoes.cshtml", restaurante);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { Descricao = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
