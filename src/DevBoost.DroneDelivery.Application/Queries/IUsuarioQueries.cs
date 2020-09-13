using DevBoost.DroneDelivery.Application.ViewModels;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public interface IUsuarioQueries
    {
        Task<UsuarioViewModel> ObterPorCredenciais(string username, string password);
        Task<UsuarioViewModel> ObterPorNome(string username);
    }
}
