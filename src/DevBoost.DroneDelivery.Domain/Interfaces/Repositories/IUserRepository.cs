using DevBoost.DroneDelivery.Core.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Entities;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterCredenciais(string username, string password);
        Task<Usuario> ObterPorNome(string username);
    }
}
