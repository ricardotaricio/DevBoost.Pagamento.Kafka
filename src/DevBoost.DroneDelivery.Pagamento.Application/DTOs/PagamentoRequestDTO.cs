using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Pagamento.Application.DTOs
{
    public class PagamentoRequestDTO
    {
        public Guid PagamentoId { get; set; }
        public Guid PedidoId { get; set; }
        public double Valor { get; set; }
        public string NumeroCartao { get; set; }
        public string Bandeira { get; set; }
        public short MesVencimento { get; set; }
        public short AnoVencimento { get; set; }
    }
}
