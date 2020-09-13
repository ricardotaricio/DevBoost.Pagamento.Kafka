using MediatR;
using System;

namespace DevBoost.DroneDelivery.Core.Domain.Messages
{
    public class Event : Message, INotification
    {
        public Event(Guid entityId)
        {
            Timestamp = DateTime.Now;
            EntityId = entityId;
        }

        public DateTime Timestamp { get; private set; }
        public Guid EntityId { get; set; }
    }
}
