using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuardFood.Client.Controllers
{
    public class MidiaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMidiaRepository _midiaRepository;

        public MidiaController(UserManager<Usuario> userManager, IUsuarioRepository usuarioRepository, IMidiaRepository midiaRepository)
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _midiaRepository = midiaRepository;
        }

        [AllowAnonymous]
        public ActionResult GetMidia(Guid id)
        {
            var midia = _midiaRepository.GetById(id);
            return File(midia.Arquivo, $"application/{midia.Extensao}");
        }
    }
}
