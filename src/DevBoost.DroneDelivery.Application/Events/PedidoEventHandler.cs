using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoAdicionadoEvent>
    {
        public async Task Handle(PedidoAdicionadoEvent message, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
                 await client.PostAsync("https://localhost/api/pagamento", new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json"));

        }
    }
}
