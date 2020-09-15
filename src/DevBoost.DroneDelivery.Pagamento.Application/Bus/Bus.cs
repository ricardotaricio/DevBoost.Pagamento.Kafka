using Confluent.Kafka;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Application.Bus
{
    public class Bus : IBus
    {
        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = await producer
                                        .ProduceAsync("pagamentos-processados", new Message<Null, string> { Value = JsonConvert.SerializeObject(evento) });
                                            //.GetAwaiter()
                                            //    .GetResult();

                    // return $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return;
        }
    }
}
