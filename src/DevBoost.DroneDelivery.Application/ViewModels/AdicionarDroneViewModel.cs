using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    [ExcludeFromCodeCoverage]

    public class AdicionarDroneViewModel
    {
        
        public int Capacidade { get; set; }

        public int Velocidade { get; set; }
        
        public int Autonomia { get; set; }

    }
}
