using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {

        Task<IEnumerable<Pedido>> ObterPedidosEmAberto();
        Task<IEnumerable<Pedido>> ObterPedidosEmTransito();

    }
}
