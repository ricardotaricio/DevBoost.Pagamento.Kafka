using System;
using System.Threading.Tasks;
using AutoMapper;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using Microsoft.AspNetCore.Mvc;


namespace DevBoost.DroneDelivery.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteQueries _clienteQueries;
        private readonly IMediatrHandler _mediatr;
        private readonly IMapper _mapper;
        public ClienteController(IMediatrHandler mediatr, IClienteQueries clienteQueries, IMapper mapper)
        {
            _clienteQueries = clienteQueries;
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var clientes = await _clienteQueries.ObterTodos();

            if (clientes == null) BadRequest();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var cliente = await _clienteQueries.ObterPorId(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarClienteViewModel clienteView)
        {

            var sucesso = await _mediatr.EnviarComando(_mapper.Map<AdicionarClienteCommand>(clienteView));

            if (!sucesso) return BadRequest();

            return Ok();
        }
    }
}
