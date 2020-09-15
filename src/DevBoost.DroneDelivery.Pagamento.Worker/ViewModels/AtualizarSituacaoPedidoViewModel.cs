using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Pagamento.Worker.ViewModels
{
    public class AtualizarSituacaoPedidoViewModel
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public int SituacaoPagamento { get; set; }
    }
}
