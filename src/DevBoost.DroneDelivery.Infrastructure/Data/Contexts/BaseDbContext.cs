using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Contexts
{
    public class BaseDbContext : DbContext, IUnitOfWork
    {

        public BaseDbContext()
        {
                
        }
        private readonly IMediatrHandler _bus;

        public BaseDbContext(DbContextOptions options, IMediatrHandler bus) : base(options)
        {
            _bus = bus;
        }

        public async Task<bool> Commit()
        {
            var executado = await base.SaveChangesAsync() > 0;

            if (executado) await _bus.PublicarEventos(this);

            return executado;
        }
    }
}
