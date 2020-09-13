using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Core.Domain.Messages
{
    public abstract class Message
    {
        public Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }
        public Guid Id { get; set; }
    }
}
