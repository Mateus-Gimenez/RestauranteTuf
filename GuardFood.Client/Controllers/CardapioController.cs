using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Data.ViewModel;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using Rotativa.AspNetCore;

namespace GuardFood.Client.Controllers
{
    [Authorize]
    public class CardapioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRestauranteRepository _restauranteRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public CardapioController(UserManager<Usuario> userManager, IRestauranteRepository restauranteRepository, IMesaRepository mesaRepository, IPedidoRepository pedidoRepository)
        {
            _userManager = userManager;
            _restauranteRepository = restauranteRepository;
            _mesaRepository = mesaRepository;
            _pedidoRepository = pedidoRepository;
        }

        [AllowAnonymous]
        [Route("/Cardapio")]
        public IActionResult Cardapio(Guid id)
        {
            var restaurante = _restauranteRepository.GetById(id);
            ViewData["Categorias"] = _restauranteRepository.GetCardapio(restaurante.Id);
            return View(restaurante);
        }

        [AllowAnonymous]
        [Route("/Cardapio/Pedido")]
        public IActionResult CardapioPedido(Guid Mesa)
        {
            var mesa = _mesaRepository.BuscarPorId(Mesa);
            var restaurante = _restauranteRepository.GetById(mesa.RestauranteId);
            mesa.Restaurante = restaurante;

            var cardapio = _restauranteRepository.GetCardapio(restaurante.Id);

            var pedido = new Pedido() { RestauranteId = restaurante.Id, MesaId = mesa.Id };
            var pedidoProdutos = new List<PedidoProduto>();

            var pedidoSalvo = Request.Cookies[$"pedido-{restaurante.Id}"];
            if (!string.IsNullOrWhiteSpace(pedidoSalvo))
            {
                var pedidoInfo = new PedidoInfoViewModel();
                JsonConvert.PopulateObject(pedidoSalvo, pedidoInfo);

                pedido.NomeCliente = pedidoInfo?.Pedido?.NomeCliente;
                pedido.Telefone = pedidoInfo?.Pedido?.Telefone;

                var produtos = new List<Produto>();
                foreach(var p in cardapio.Select(s => s.Produtos))
                {
                    produtos.AddRange(p);
                }

                foreach(var pp in pedidoInfo?.PedidoProdutos ?? new List<PedidoProdutoViewModel>())
                {
                    var produto = produtos.FirstOrDefault(f => f.Id == pp.ProdutoId);
                    if(produto != null)
                    {
                        pedidoProdutos.Add(new PedidoProduto() 
                        { 
                            ProdutoId = pp.ProdutoId,
                            PedidoId = pedido.Id,
                            Quantidade = pp.Quantidade,
                            RestauranteId = pedido.RestauranteId,
                            NomeProduto = produto.Nome,
                            ValorUnitario = produto.Valor,
                            Observacao = pp.Observacao,
                        });
                    }
                }
            }

            ViewData["Categorias"] = cardapio;
            ViewData["Pedido"] = pedido;
            ViewData["PedidoProdutos"] = pedidoProdutos;

            return View(mesa);
        }

        [HttpGet]
        [Route("/Cardapio/PDF")]
        public IActionResult CardapioPDF(Guid Mesa)
        {
            var mesa = _mesaRepository.BuscarPorId(Mesa);
            mesa.Restaurante = _restauranteRepository.GetById(mesa.RestauranteId);

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode($"{Request.Scheme}://{Request.Host}/Cardapio/Pedido?Mesa={mesa.Id}", QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(6);
            mesa.Base64QrCode = Convert.ToBase64String(qrCode.GetGraphic(6));

            var pdf = new ViewAsPdf("_PDFCardapioMesaQRCode", mesa)
            {
                FileName = $"{mesa.Restaurante.Nome} - {mesa.Codigo}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                ContentDisposition = Rotativa.AspNetCore.Options.ContentDisposition.Inline,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(1, 1, 1, 1),
            };

            return pdf;
        }

        [HttpGet]
        [Route("/StatusPedido")]
        [AllowAnonymous]
        public IActionResult StatusPedido(Guid restauranteId, string telefone)
        {
            var restaurante = _restauranteRepository.GetById(restauranteId);

            var numeros = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var numeroTelefone = "";

            foreach (var t in telefone ?? "")
            {
                if (numeros.Contains(t))
                {
                    numeroTelefone += t;
                }
            }

            if (numeroTelefone.Length != 11 || telefone.Length != 11)
            {
                return View("~/Views/StatusPedido/InserirTelefone.cshtml", restaurante);
            }

            ViewData["Pedidos"] = _pedidoRepository.GetByTelefone(restaurante.Id, telefone);
            ViewData["Telefone"] = telefone;
            ViewData["TelefoneFormatado"] = Convert.ToUInt64(telefone).ToString(@"(00) 00000-0000");

            return View("~/Views/StatusPedido/ListaPedido.cshtml", restaurante);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/SalvaInfoPedido")]
        public void SalvaInfoPedido(string pedidoInfoViewModel, Guid restauranteId)
        {
            HttpContext.Response.Cookies.Append($"pedido-{restauranteId}", pedidoInfoViewModel);
        }
    }
}
