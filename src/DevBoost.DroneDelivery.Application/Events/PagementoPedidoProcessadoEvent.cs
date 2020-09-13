using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Events
{
    [ExcludeFromCodeCoverage]
    public class PagementoPedidoProcessadoEvent : Event
    {
        public Guid PagamentoId { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }

        public PagementoPedidoProcessadoEvent(Guid entityId, SituacaoPagamento situacaoPagamento) : base(entityId)
        {
            SituacaoPagamento = situacaoPagamento;
        }
    }
}
