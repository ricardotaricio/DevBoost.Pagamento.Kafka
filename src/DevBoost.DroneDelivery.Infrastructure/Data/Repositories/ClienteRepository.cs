using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly DCDroneDelivery _context;

        public ClienteRepository(DCDroneDelivery context) : base(context)
        {
            _context = context;
        }

    }
}
