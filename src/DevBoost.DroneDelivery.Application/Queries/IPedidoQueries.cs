using DevBoost.DroneDelivery.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<PedidoViewModel>> ObterTodos();
        Task<IEnumerable<PedidoViewModel>> ObterPedidosEmTransito();
        Task<IEnumerable<PedidoViewModel>> ObterPedidosEmTransitoPorDrone(Guid drone);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosEmAberto();
    }
}
