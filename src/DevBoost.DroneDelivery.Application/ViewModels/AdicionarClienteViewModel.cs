using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AdicionarClienteViewModel
    {
        public string Nome { get; set; }
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        public string Usuario { get; set; }

       
        public string Senha { get; set; }
    }
}
