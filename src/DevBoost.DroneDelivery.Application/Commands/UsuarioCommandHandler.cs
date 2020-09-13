using AutoMapper;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Extensions;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class UsuarioCommandHandler : IRequestHandler<AdicionarUsuarioCommand, bool>
    {
        private IMediatrHandler _mediatr;
        private IUserRepository _usuariorRepository;
        private IClienteQueries _clienteQueries;
        private IMapper _mapper;
        public UsuarioCommandHandler(IClienteQueries clienteQueries, IMapper mapper, IMediatrHandler mediatr, IUserRepository userRepository)
        {
            _mediatr = mediatr;
            _mapper = mapper;
            _clienteQueries = clienteQueries;
            _usuariorRepository = userRepository;
        }

        public async Task<bool> Handle(AdicionarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var usuario = _mapper.Map<Usuario>(message);
            usuario.Password = usuario.Password.ObterHash();
            await _usuariorRepository.Adicionar(usuario);

            return await _usuariorRepository.UnitOfWork.Commit();
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
