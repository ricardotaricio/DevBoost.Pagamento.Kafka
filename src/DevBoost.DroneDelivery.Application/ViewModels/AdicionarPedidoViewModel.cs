using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AdicionarPedidoViewModel
    {
        
        public int Peso { get; set; }
        public string NumeroCartao { get; set; }
        public string Bandeira { get; set; }
        public short MesVencimento { get; set; }
        public short AnoVencimento { get; set; }
        public double Valor { get; set; }
    }
}
