using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using DevBoost.DroneDelivery.Pagamento.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Contexts;

namespace DevBoost.DroneDelivery.Pagamento.Infrastructure.Data.Repositories
{
    public class PagamentoRepository : Repository<PagamentoCartao>, IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context): base(context)
        {
            _context = context;
        }
    }
}
