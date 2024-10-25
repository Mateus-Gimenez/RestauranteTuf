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
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<Usuario> userManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View();
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
