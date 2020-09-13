using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Pagamento.Application.ViewModels
{
    public class AtualizarSituacaoPedidoViewModel
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
    }
}
