using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Worker.BackgroundWorker
{
    public class PedidoBackground : BackgroundService
    {
        private readonly ILogger<PedidoBackground> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private Consumer _consumer;

        public PedidoBackground(ILogger<PedidoBackground> logger)
        {
            _kafkaOptions = new KafkaOptions(new Uri("http://localhost:9092"));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new Consumer(new ConsumerOptions("pedidoteste", _brokerRouter));
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => _logger.LogDebug($"{DateTime.Now} | Serviço parado..."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogDebug($"{DateTime.Now} | Serviço em execução... ");
                    await ObterAsync();
                }
                catch (Exception)
                {

                    
                }
                
            }

        }
        public ByteArrayContent ConvertObjectToByteArrayContent(string valor)
        {
            ByteArrayContent byteContent = new ByteArrayContent((Encoding.UTF8.GetBytes(valor)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        private async Task ObterAsync()
        {
            foreach (var msg in _consumer.Consume())
                using (HttpClient client = new HttpClient())
                    await client.PostAsync("http://localhost:50648/api/pedido", ConvertObjectToByteArrayContent(Encoding.UTF8.GetString(msg.Value)));
        }
    }
}
