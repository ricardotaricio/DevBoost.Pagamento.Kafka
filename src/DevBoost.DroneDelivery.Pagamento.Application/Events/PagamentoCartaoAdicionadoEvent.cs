using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.Events
{
    public class PagamentoCartaoAdicionadoEvent : Event
    {
        public PagamentoCartaoAdicionadoEvent(Guid entityId) : base(entityId)
        {
            
        }

        
    }
}
