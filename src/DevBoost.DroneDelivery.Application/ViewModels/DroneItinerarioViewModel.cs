using DevBoost.Dronedelivery.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    public class DroneItinerarioViewModel
    {
        public DateTime DataHora { get; private set; }
        public EnumStatusDrone StatusDrone { get; private set; }
        public DroneViewModel  Drone { get; set; }
    }
}
