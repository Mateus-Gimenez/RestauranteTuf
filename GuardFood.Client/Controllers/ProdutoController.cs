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

        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _produtoRepository.BuscarTodos();
        }

        [HttpPost]
        public RetornoViewModel Post(string values)
        {
            try
            {
                var objeto = new Produto();
                JsonConvert.PopulateObject(values, objeto);

                return _produtoRepository.InserirEditar(objeto);
            }
            catch (Exception e)
            {
                return new RetornoViewModel()
                {
                    Sucesso = false,
                    Mensagem = e.Message
                };
            }
        }

        [HttpPut]
        public RetornoViewModel Put(Guid key, string values)
        {
            try
            {
                var objeto = _produtoRepository.BuscarPorId(key);
                JsonConvert.PopulateObject(values, objeto);

                return _produtoRepository.InserirEditar(objeto);
            }
            catch (Exception e)
            {
                return new RetornoViewModel()
                {
                    Sucesso = false,
                    Mensagem = e.Message
                };
            }
        }

        [HttpDelete]
        public RetornoViewModel Delete(Guid key)
        {
            return _produtoRepository.Deletar(key);
        }
    }
}
