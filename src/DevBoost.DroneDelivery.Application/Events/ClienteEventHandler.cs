using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteAdiconadoEvent>
    {

        private readonly IMediatrHandler _mediatr;

        public ClienteEventHandler(IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Handle(ClienteAdiconadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatr.EnviarComando(new AdicionarUsuarioCommand(message.Nome, message.Senha, message.EntityId,"User"));
        }
    }
}
