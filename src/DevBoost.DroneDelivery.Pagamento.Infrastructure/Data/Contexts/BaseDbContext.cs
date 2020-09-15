using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Contexts
{
    public class BaseDbContext : DbContext, IUnitOfWork
    {

        public BaseDbContext()
        {
                
        }
        private readonly IMediatrHandler _mediator;

        public BaseDbContext(DbContextOptions options, IMediatrHandler mediator) : base(options)
        {
            _mediator = mediator;
        }

        public async Task<bool> Commit()
        {
            var executado = await base.SaveChangesAsync() > 0;

            if (executado) await _mediator.PublicarEventos(this);

            return executado;
        }
    }
}
