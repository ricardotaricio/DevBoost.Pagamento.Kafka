using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Events
{

    public class DroneEventHandler : INotificationHandler<AutonomiaAtualizadaDroneEvent>,INotificationHandler<DroneAdicionadoEvent>
    {
        private readonly IMediatrHandler _mediatr;
        public DroneEventHandler(IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Handle(AutonomiaAtualizadaDroneEvent message, CancellationToken cancellationToken)
        {
           
        }

        
        public async Task Handle(DroneAdicionadoEvent message, CancellationToken cancellationToken)
        {

            await _mediatr.EnviarComando(new AdicionarDroneItinerarioCommand(DateTime.Now, message.EntityId, EnumStatusDrone.Disponivel));

        }
    }
}
