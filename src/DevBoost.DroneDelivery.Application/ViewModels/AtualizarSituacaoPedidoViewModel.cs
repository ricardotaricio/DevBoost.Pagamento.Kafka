using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.ViewModels
{

    [ExcludeFromCodeCoverage]
    public class AtualizarSituacaoPedidoViewModel
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public SituacaoPagamento SituacaoPagamento { get; set; }
    }
}
