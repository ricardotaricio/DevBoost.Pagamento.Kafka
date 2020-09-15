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
        private readonly IMediatrHandler _mediator;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoQueries _pagamentoQueries;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public PagamentoCommandHandler(IBus bus, IMediatrHandler mediator, IPagamentoRepository pagamentoRepository, IPagamentoQueries pagamentoQueries, IMapper mapper)
        {
            _mediator = mediator;
            _pagamentoRepository = pagamentoRepository;
            _pagamentoQueries = pagamentoQueries;
            _mapper = mapper;
            _bus = bus;
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

            var evento = new PagamentoCartaoProcessadoEvent(entityId: pagamento.Id, pagamento.PedidoId, pagamento.Situacao);
            pagamento.AdicionarEvento(evento);
            var pagamentoAtualizado = await _pagamentoRepository.UnitOfWork.Commit();
            if (pagamentoAtualizado) await _bus.PublicarEvento<PagamentoCartaoProcessadoEvent>(evento);

            return pagamentoAtualizado;
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

    }
}
