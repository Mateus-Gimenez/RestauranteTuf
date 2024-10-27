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
    public class ProdutoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(UserManager<Usuario> userManager, IProdutoRepository produtoRepository)
        {
            _userManager = userManager;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["RestauranteId"] = _userManager.GetUserAsync(User).Result.RestauranteId;
            return View();
        }
    }
}
