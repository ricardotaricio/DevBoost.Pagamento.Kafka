using DevBoost.DroneDelivery.Core.Domain.Entities;
using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Domain.Entites
{
    public class PagamentoCartao : Entity
    {
        public PagamentoCartao(): base()
        {

        }

        public Guid PedidoId { get; set; }
        public double Valor { get; set; }
        public SituacaoPagamento Situacao { get; set; }

        public Guid CartaoId { get; set; }
        public Cartao  Cartao { get; set; }


    }
}
