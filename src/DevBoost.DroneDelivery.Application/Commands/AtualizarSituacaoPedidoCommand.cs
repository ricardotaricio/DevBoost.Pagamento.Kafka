using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AtualizarSituacaoPedidoCommand : Command
    {

        public AtualizarSituacaoPedidoCommand(Guid pedidoId , EnumStatusPedido statusPedido)
        {
            PedidoId = pedidoId;
            StatusPedido = statusPedido;
        }

        public Guid PedidoId { get; private set; }

        public EnumStatusPedido StatusPedido { get; private set; }

    }
}
