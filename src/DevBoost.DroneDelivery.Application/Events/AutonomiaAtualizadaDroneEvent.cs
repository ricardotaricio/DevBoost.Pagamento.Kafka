using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Events
{
    [ExcludeFromCodeCoverage]
    public class AutonomiaAtualizadaDroneEvent : Event
    {

        public AutonomiaAtualizadaDroneEvent(Guid entityId , int autonomiaRestante ) :base(entityId)
        {
            AutonomiaRestante = autonomiaRestante;
        }

        public int AutonomiaRestante { get; private set; }
    }
}
