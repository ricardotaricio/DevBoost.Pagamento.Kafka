using DevBoost.DroneDelivery.Pagamento.Worker.ViewModels;
using KafkaNet;
using KafkaNet.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Worker.BackgroundWorker
{
    public class PagamentoBackground : BackgroundService
    {
        private readonly ILogger<PagamentoBackground> _logger;
        private KafkaOptions _kafkaOptions;
        private BrokerRouter _brokerRouter;
        private Consumer _consumer;

        public PagamentoBackground(ILogger<PagamentoBackground> logger)
        {
            _kafkaOptions = new KafkaOptions(new Uri("http://localhost:9092"));
            _brokerRouter = new BrokerRouter(_kafkaOptions);
            _consumer = new Consumer(new ConsumerOptions("pagamentos-processados", _brokerRouter));
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
                {
                    var msgObject = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(msg.Value));

                    var atualizarSituacaoPedidoViewModel = new AtualizarSituacaoPedidoViewModel()
                    {
                        PedidoId = msgObject.PedidoId,
                        PagamentoId = msgObject.EntityId,
                        SituacaoPagamento = msgObject.SituacaoPagamento
                    };

                    await client.PatchAsync("http://localhost:50648/api/pedido", ConvertObjectToByteArrayContent(JsonConvert.SerializeObject(atualizarSituacaoPedidoViewModel)));
                }
        }
    }
}
