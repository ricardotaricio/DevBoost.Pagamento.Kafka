using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace DevBoost.DroneDelivery.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IUsuarioQueries _usuarioQueries;
        private readonly IClienteQueries _clienteQueries;
        private readonly IMediatrHandler _mediator;

        public PedidoController(IPedidoQueries pedidoQueries, IUsuarioQueries usuarioQueries, IClienteQueries clienteQueries, IMediatrHandler mediatr)
        {
            _usuarioQueries = usuarioQueries;
            _clienteQueries = clienteQueries;
            _mediator = mediatr;
            _pedidoQueries = pedidoQueries;

        }



        [HttpGet]
        public async Task<IActionResult> GetPedido()
        {
            return Ok(await _pedidoQueries.ObterTodos());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(Guid id)
        {

            var pedido = await _pedidoQueries.ObterPorId(id);

            if (pedido == null) return NotFound();

            return Ok(pedido);
        }


        [HttpPost]
        public async Task<IActionResult> AdicionarPedido(AdicionarPedidoViewModel pedidoViewModel)
        {


            var username = User.Identities.FirstOrDefault().Name;
            var user = await _usuarioQueries.ObterPorNome(username);

            if (user == null)
                return BadRequest();

            var cliente = await _clienteQueries.ObterPorId(user.ClienteId);

            var retorno = await _mediator.EnviarComando(new AdicionarPedidoCommand(cliente.Id, pedidoViewModel.Valor, pedidoViewModel.Peso, DateTime.Now, pedidoViewModel.Bandeira, pedidoViewModel.NumeroCartao, pedidoViewModel.MesVencimento, pedidoViewModel.AnoVencimento));
            
            if (!retorno) return BadRequest();

            return Ok();
        }


        [HttpPatch]
        public async Task<IActionResult> AtualizarSituacaoPedido(AtualizarSituacaoPedidoViewModel viewModel)
        {
            await _mediator.PublicarEvento(new PagementoPedidoProcessadoEvent(viewModel.PedidoId, viewModel.SituacaoPagamento));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> DespacharPedidos()
        {
            return Ok(await _mediator.EnviarComando(new DespacharPedidoCommand()));
        }
    }
}
