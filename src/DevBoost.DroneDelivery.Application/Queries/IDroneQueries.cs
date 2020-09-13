using DevBoost.DroneDelivery.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{

    public interface IDroneQueries
    {
        Task<DroneViewModel> ObterPorId(Guid id);
        Task<IEnumerable<DroneViewModel>> ObterTodos();
    }
}
