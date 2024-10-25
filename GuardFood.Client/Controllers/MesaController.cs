using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuardFood.Client.Controllers
{
    [Authorize]
    public class MesaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;

        public MesaController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View();
        }
    }
}
