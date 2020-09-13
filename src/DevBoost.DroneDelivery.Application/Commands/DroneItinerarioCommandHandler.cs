using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class DroneItinerarioCommandHandler : IRequestHandler<AdicionarDroneItinerarioCommand, bool>
    {
        private readonly IMediatrHandler _mediatr;
        private readonly IMapper _mapper;
        private readonly IDroneItinerarioRepository _droneItinerarioRepository;
        public DroneItinerarioCommandHandler(IDroneItinerarioRepository droneItinerarioRepository,IMapper mapper,IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
            _mapper = mapper;
            _droneItinerarioRepository = droneItinerarioRepository;
        }
        public async Task<bool> Handle( AdicionarDroneItinerarioCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            //TODO: Itinerario adicionando duas vezes!!!

            await _droneItinerarioRepository.Adicionar(_mapper.Map<DroneItinerario>(message));
            return await _droneItinerarioRepository.UnitOfWork.Commit();
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
