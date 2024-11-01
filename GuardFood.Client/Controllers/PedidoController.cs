using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuardFood.Client.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(UserManager<Usuario> userManager, IPedidoRepository pedidoRepository)
        {
            _userManager = userManager;
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View();
        }
    }
}
