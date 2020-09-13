using DevBoost.DroneDelivery.Pagamento.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Application.Queries
{
    public interface IPagamentoQueries
    {
        Task<PagamentoCartao> ObterPorId(Guid id);
    }
}
