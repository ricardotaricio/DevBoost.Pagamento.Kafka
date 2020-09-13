using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using AutoMapper;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Queries;
using System;

namespace DevBoost.DroneDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {

    
        private readonly IMediatrHandler _mediatr;
        private readonly IMapper _mapper;
        private readonly IDroneQueries _droneQueries;

        public DroneController(IMediatrHandler mediatr, IMapper mapper, IDroneQueries droneQueries)
        {
            _mediatr = mediatr;
            _mapper = mapper;
            _droneQueries = droneQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrone()
        {
            //await _pedidoService.DespacharPedidos();

            //var drones = await _droneQueries.ObterTodos();

            //var situacaoDrones = new List<SituacaoDroneDTO>();


            //var dronesItinerario = _droneItinerarioService.GetAll().Result;

            //var pedidosEmTransito = await _pedidoService.GetPedidosEmTransito();

            //foreach (var drone in drones)
            //{
            //    var situacaoDrone = new SituacaoDroneDTO { Drone = drone };

            //    var droneItinerario = dronesItinerario.SingleOrDefault(x => x.DroneId == drone.Id);

            //    if (droneItinerario == null)
            //        situacaoDrone.StatusDrone = EnumStatusDrone.Disponivel.ToString();
            //    else
            //        situacaoDrone.StatusDrone = droneItinerario.StatusDrone.ToString();

            //    situacaoDrone.Pedidos = pedidosEmTransito.Where(p => p.Drone != null && p.Status != EnumStatusPedido.Entregue && p.Drone.Id == drone.Id).ToList();

            //    situacaoDrones.Add(situacaoDrone);
            //}

            //return Ok(situacaoDrones);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrone(Guid id)
        {
            var drone = await _droneQueries.ObterPorId(id);

            if (drone == null)
                return NotFound();

            return Ok(drone);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostDrone(AdicionarDroneViewModel droneViewModel)
        {
            var sucesso = await _mediatr.EnviarComando(_mapper.Map<AdicionarDroneCommand>(droneViewModel));

            if (!sucesso) return BadRequest();

            return Ok();


        }
    }
}
