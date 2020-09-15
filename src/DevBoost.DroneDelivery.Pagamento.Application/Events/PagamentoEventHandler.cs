using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.DTOs;
using DevBoost.DroneDelivery.Pagamento.Application.Queries;
using MediatR;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Application.Events
{
    public class PagamentoEventHandler : INotificationHandler<PagamentoCartaoAdicionadoEvent>
    {
        private readonly IPagamentoQueries _pagamentoQueries;
        private readonly IMediatrHandler _mediator;
        private readonly IMapper _mapper;

        public PagamentoEventHandler(IPagamentoQueries pagamentoQueries, IMediatrHandler mediator, IMapper mapper)
        {
            _pagamentoQueries = pagamentoQueries;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(PagamentoCartaoAdicionadoEvent  message, CancellationToken cancellationToken)
        {
            var pagamentoCartao = await _pagamentoQueries.ObterPorId(message.EntityId);

            if (pagamentoCartao == null)
                return;

            var body = _mapper.Map<PagamentoRequestDTO>(pagamentoCartao);
           
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("https://5f542997e5de110016d51dec.mockapi.io/v1/pagamento", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var objResponse = JsonConvert.DeserializeObject<PagamentoResponseDTO>(await response.Content.ReadAsStringAsync());
                    objResponse.PagamentoId = pagamentoCartao.Id;
                    await _mediator.EnviarComando(_mapper.Map<AtualizarSituacaoPagamentoCartaoCommand>(objResponse));
                }
            }
        }

        public async Task Handle(PagamentoCartaoProcessadoEvent message, CancellationToken cancellationToken)
        {
            //var body = _mapper.Map<AtualizarSituacaoPedidoDTO>(message);

            //using (HttpClient client = new HttpClient())
            //    await client.PatchAsync("https://localhost/api/pedido", new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }
    }
}
