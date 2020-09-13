using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using DevBoost.DroneDelivery.Pagamento.Application.Events;
using DevBoost.DroneDelivery.Pagamento.Application.Queries;
using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using DevBoost.DroneDelivery.Pagamento.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Application.Commands
{
    public class PagamentoCommandHandler : IRequestHandler<AdicionarPagamentoCartaoCommand, bool>, IRequestHandler<AtualizarSituacaoPagamentoCartaoCommand, bool>
    {
        private readonly IMediatrHandler _bus;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoQueries _pagamentoQueries;
        private readonly IMapper _mapper;
        public PagamentoCommandHandler(IMediatrHandler bus, IPagamentoRepository pagamentoRepository, IPagamentoQueries pagamentoQueries, IMapper mapper)
        {
            _bus = bus;
            _pagamentoRepository = pagamentoRepository;
            _pagamentoQueries = pagamentoQueries;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AdicionarPagamentoCartaoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pagamentoCartao = _mapper.Map<PagamentoCartao>(message);
            await _pagamentoRepository.Adicionar(pagamentoCartao);
            
            pagamentoCartao.AdicionarEvento(new PagamentoCartaoAdicionadoEvent(pagamentoCartao.Id));
            return await _pagamentoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarSituacaoPagamentoCartaoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pagamento = await _pagamentoQueries.ObterPorId(message.PagamentoId);
            pagamento.Situacao = message.SituacaoPagamneto;
            
            await _pagamentoRepository.Atualizar(pagamento);

            pagamento.AdicionarEvento(new PagamentoCartaoProcessadoEvent(entityId: pagamento.Id,pagamento.PedidoId, pagamento.Situacao));
            return await _pagamentoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

    }
}
