using GuardFood.Core.Data.Interfaces;
using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public CardapioController(UserManager<Usuario> userManager, IRestauranteRepository restauranteRepository, IMesaRepository mesaRepository)
        {
            _userManager = userManager;
            _restauranteRepository = restauranteRepository;
            _mesaRepository = mesaRepository;
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

            var pedido = new Pedido() { RestauranteId = restaurante.Id, MesaId = mesa.Id };

            var pedidoProdutos = new List<PedidoProduto>();

            ViewData["Categorias"] = _restauranteRepository.GetCardapio(restaurante.Id);
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
    }
}
