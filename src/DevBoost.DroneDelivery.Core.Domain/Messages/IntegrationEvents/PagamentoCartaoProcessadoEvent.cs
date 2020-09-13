using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents
{
    public class PagamentoCartaoProcessadoEvent : Event
    {
        public PagamentoCartaoProcessadoEvent(Guid entityId,Guid pedidoId, SituacaoPagamento situacaoPagamento) : base(entityId)
        {
            PedidoId = pedidoId;
            SituacaoPagamento = situacaoPagamento;
        }

        public Guid PedidoId { get; private set; }
        public SituacaoPagamento SituacaoPagamento { get; private set; }
    }
}
