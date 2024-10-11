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
            return View();
        }

        [HttpGet]
        public IEnumerable<Pedido> Get()
        {
            return _pedidoRepository.BuscarTodos();
        }

        [HttpPost]
        public RetornoViewModel Post(string values)
        {
            try
            {
                var objeto = new Pedido();
                JsonConvert.PopulateObject(values, objeto);

                return _pedidoRepository.InserirEditar(objeto);
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
                var objeto = _pedidoRepository.BuscarPorId(key);
                JsonConvert.PopulateObject(values, objeto);

                return _pedidoRepository.InserirEditar(objeto);
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
            return _pedidoRepository.Deletar(key);
        }
    }
}
