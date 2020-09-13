using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Events
{

    public class PagementoEventHandler : IHandleMessages<PagamentoCartaoProcessadoEvent>
    {
        private readonly IMediatrHandler _mediatr;

        public PagementoEventHandler(IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
        }

        
        public async Task Handle(PagamentoCartaoProcessadoEvent message)
        {
            if (message.SituacaoPagamento == SituacaoPagamento.Aguardando) return;

            var statusPedido = message.SituacaoPagamento == SituacaoPagamento.Autorizado ? EnumStatusPedido.AguardandoEntregador : EnumStatusPedido.PagamentoRejeitado;

            await _mediatr.EnviarComando(new AtualizarSituacaoPedidoCommand(message.EntityId, statusPedido));
        }

        
    }
}
