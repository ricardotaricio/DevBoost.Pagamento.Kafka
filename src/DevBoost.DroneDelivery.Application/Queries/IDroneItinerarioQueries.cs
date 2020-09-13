using DevBoost.DroneDelivery.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public interface IDroneItinerarioQueries
    {
        Task<DroneItinerarioViewModel> ObterPorId(Guid id);
        Task<IEnumerable<DroneItinerarioViewModel>> ObterTodos();
        Task<IEnumerable<DroneItinerarioViewModel>> ObterDronesIndisponiveis();
        Task<IEnumerable<DroneViewModel>> ObterDronesDisponiveis();
    }
}
