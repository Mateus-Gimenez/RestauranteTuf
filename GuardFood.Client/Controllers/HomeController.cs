using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GuardFood.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRestauranteRepository _restauranteRepository;

        public HomeController(UserManager<Usuario> userManager, IRestauranteRepository restauranteRepository)
        {
            _userManager = userManager;
            _restauranteRepository = restauranteRepository;
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

        [AllowAnonymous]
        [Route("/Cardapio")]
        public IActionResult Cardapio(Guid id)
        {
            var restaurante = _restauranteRepository.GetById(id);
            ViewData["Categorias"] = _restauranteRepository.GetCardapio(restaurante.Id);
            return View("~/Views/Pedido/Cardapio.cshtml", restaurante);
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
