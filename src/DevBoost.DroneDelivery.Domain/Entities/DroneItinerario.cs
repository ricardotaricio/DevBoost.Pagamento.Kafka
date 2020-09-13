using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class DroneItinerario : Entity
    {
        public DroneItinerario(DateTime dataHora,Guid droneId, EnumStatusDrone statusDrone)
        {
            DataHora = dataHora;
            DroneId = droneId;
            StatusDrone = statusDrone;
        }

        public DateTime DataHora { get; private set; }
        
        public Guid DroneId { get; private set; }
        public EnumStatusDrone StatusDrone { get; private set; }
        public int Controle { get; set; }
        public Drone Drone { get; set; }
        public void InformarStatusDrone(EnumStatusDrone statusDrone)
        {
            this.StatusDrone = statusDrone;
        }

        public void InformarDrone(Drone drone)
        {
            this.Drone = drone;
        }

        public void InformarDataHora(DateTime data)
        {
            this.DataHora = data;
        }
    }
}
