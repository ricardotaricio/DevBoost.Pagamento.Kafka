using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class PedidoDespachadoEvent : Event
    {

        public PedidoDespachadoEvent(Guid entityId) : base(entityId)
        {
        }
    }
}
