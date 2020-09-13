using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.ViewModels
{

    [ExcludeFromCodeCoverage]
    public class AdicionarPagamentoCartaoViewModel
    {
        public Guid PedidoId { get; set; }
        public double Valor { get; set; }
        public string BandeiraCartao { get; set; }
        public string NumeroCartao { get; set; }
        public short MesVencimentoCartao { get; set; }
        public short AnoVencimentoCartao { get; set; }
    }
}
