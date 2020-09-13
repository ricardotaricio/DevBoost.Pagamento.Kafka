using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class DroneRepository : Repository<Drone>, IDroneRepository
    {
        private readonly DCDroneDelivery _context;

        public DroneRepository(DCDroneDelivery context):base(context)
        {
            _context = context;
           

        }

        //public IEnumerable<Drone> ObterSituacao()
        //{
        //    var drones = _context.Drone.ToList();

        //    //var itinerario = _context.DroneItinerario. .DroneItinerario..Where(i => i.)

        //    //_context.DroneItinerario.ToList().Select(d=>d.Drone).Distinct().
        //    return
        //}

    }
}
