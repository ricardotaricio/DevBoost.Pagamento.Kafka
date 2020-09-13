using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.DTOs
{
    public class AtualizarSituacaoPedidoDTO
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
    }
}
