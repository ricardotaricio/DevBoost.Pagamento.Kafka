using DevBoost.DroneDelivery.Core.Domain.Messages;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
