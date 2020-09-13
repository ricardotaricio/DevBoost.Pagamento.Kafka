using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using DevBoost.DroneDelivery.Pagamento.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Application.Queries
{
    public class PagamentoQueries : IPagamentoQueries
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public PagamentoQueries(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public Task<PagamentoCartao> ObterPorId(Guid id)
        {
            return _pagamentoRepository.ObterPorId(id);
        }
    }
}
