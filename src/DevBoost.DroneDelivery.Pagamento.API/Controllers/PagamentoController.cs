using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private IMediatrHandler _bus;
        private IMapper _mapper;

        public PagamentoController(IMediatrHandler bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PagamentoCartaoPost(AdicionarPagamentoCartaoViewModel viewModel)
        {
            // TODO: Verificar como retornar motivo da rejeição do pagamento a partir do service pra não precisar validar aqui, apenas no service.
            var enviado = await _bus.EnviarComando(_mapper.Map<AdicionarPagamentoCartaoCommand>(viewModel));

            if (!enviado) return BadRequest();

            return Ok();
        }
    }
}
