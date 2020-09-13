using AutoMapper;
using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class DroneCommandHandler : IRequestHandler<AdicionarDroneCommand, bool>, IRequestHandler<AtualizarAutonomiaDroneCommand, bool>, IRequestHandler<AtualizarSituacaoDroneCommand, bool>
    {
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IDroneItinerarioQueries _droneItinerarioQueries;
        private readonly IDroneRepository _droneRepository;
        private readonly IMediatrHandler _mediatr;
        private readonly IMapper _mapper;
        private readonly IDroneQueries _droneQueries;
        public DroneCommandHandler(IPedidoQueries pedidoQueries, IDroneItinerarioQueries droneItinerarioQueries, IDroneQueries droneQueries, IMapper mapper, IMediatrHandler mediatr, IDroneRepository droneRepository)
        {

            _droneRepository = droneRepository;
            _mediatr = mediatr;
            _mapper = mapper;
            _droneQueries = droneQueries;
            _pedidoQueries = pedidoQueries;
            _droneItinerarioQueries = droneItinerarioQueries;
        }

        public async Task<bool> Handle(AdicionarDroneCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;


            var drone = _mapper.Map<Drone>(message);
            await _droneRepository.Adicionar(drone);

            drone.AdicionarEvento(_mapper.Map<DroneAdicionadoEvent>(drone));
            return await _droneRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarAutonomiaDroneCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var drone = _mapper.Map<Drone>(_droneQueries.ObterPorId(message.DroneId));
            drone.InformarAutonomiaRestante(message.AutonomiaRestante);
            drone.AdicionarEvento(new AutonomiaAtualizadaDroneEvent(drone.Id, drone.AutonomiaRestante));
            return await _droneRepository.UnitOfWork.Commit();

        }

        public async Task<bool> Handle(AtualizarSituacaoDroneCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var droneItinerarios = _droneItinerarioQueries.ObterDronesIndisponiveis().Result.ToList();

            foreach (var droneItinerario in droneItinerarios)
            {


                if (droneItinerario.StatusDrone == EnumStatusDrone.Carregando)
                {
                    if (DateTime.Now.Subtract(droneItinerario.DataHora).TotalMinutes >= droneItinerario.Drone.Carga)
                        await _mediatr.EnviarComando(new AdicionarDroneItinerarioCommand(DateTime.Now, droneItinerario.Drone.Id, EnumStatusDrone.Disponivel));

                }
                else if (droneItinerario.StatusDrone == EnumStatusDrone.EmTransito)
                {
                    var pedidos = await _pedidoQueries.ObterPedidosEmTransitoPorDrone(droneItinerario.Drone.Id);


                    int tempoEntrega = CalcularTempoTotalEntregaEmMinutos(pedidos, droneItinerario.Drone);

                    if (droneItinerario.DataHora.AddMinutes(tempoEntrega) <= DateTime.Now)
                    {

                        int limiteAutonomiaParaRecarga = Convert.ToInt32(Math.Ceiling(droneItinerario.Drone.Autonomia * 0.2));

                        if (droneItinerario.Drone.AutonomiaRestante <= limiteAutonomiaParaRecarga)
                            await _mediatr.EnviarComando(new AdicionarDroneItinerarioCommand(DateTime.Now, droneItinerario.Drone.Id, EnumStatusDrone.Carregando));
                        else
                            await _mediatr.EnviarComando(new AdicionarDroneItinerarioCommand(DateTime.Now, droneItinerario.Drone.Id, EnumStatusDrone.Disponivel));

                        foreach (var pedido in pedidos)
                            await _mediatr.EnviarComando(new AtualizarSituacaoPedidoCommand(pedido.Id, EnumStatusPedido.Entregue));

                    }
                }

            }

            return true;

        }
        public int CalcularTempoTotalEntregaEmMinutos(IEnumerable<PedidoViewModel> pedidos, DroneViewModel drone)
        {
            if (!pedidos.Any())
                return 0;
            var localizacaoLoja = new Localizacao(-23.5880684, -46.6564195);
            var localizacaoOrigem = localizacaoLoja;
            Localizacao localizacaoCliente;

            double distanciaTotal = 0;

            foreach (var pedido in pedidos)
            {
                localizacaoCliente = new Localizacao(pedido.Cliente.Latitude, pedido.Cliente.Longitude);
                distanciaTotal += localizacaoOrigem.CalcularDistanciaEmKilometros(localizacaoCliente);
                localizacaoOrigem = localizacaoCliente;
            }

            distanciaTotal += localizacaoOrigem.CalcularDistanciaEmKilometros(localizacaoLoja);

            return distanciaTotal.CalcularTempoTrajetoEmMinutos(drone.Velocidade);

        }
        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatr.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
