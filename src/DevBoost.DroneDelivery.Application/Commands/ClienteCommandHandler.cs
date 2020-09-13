using AutoMapper;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class ClienteCommandHandler : IRequestHandler<AdicionarClienteCommand, bool>
    {
        private IClienteRepository _clienteRepository;
        private IMediatrHandler _mediatr;
        private IMapper _mapper;




        public ClienteCommandHandler(IClienteRepository clienteRepository, IMediatrHandler mediatr, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AdicionarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var cliente = _mapper.Map<Cliente>(message);
            await _clienteRepository.Adicionar(cliente);
            var usuario = _mapper.Map<Usuario>(message);

            cliente.AdicionarEvento(new ClienteAdiconadoEvent(cliente.Id, cliente.Nome, usuario.UserName, usuario.Password, cliente.Latitude, cliente.Longitude));
            await _clienteRepository.UnitOfWork.Commit();

            return true;
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
