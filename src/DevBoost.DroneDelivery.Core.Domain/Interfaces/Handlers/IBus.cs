using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers
{
    public interface IBus
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
