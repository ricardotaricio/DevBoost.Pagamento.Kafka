using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
