using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IDroneItinerarioRepository : IRepository<DroneItinerario>
    {
        Task<DroneItinerario> ObterDroneItinerarioPorIdDrone(Guid id);
    }
}
