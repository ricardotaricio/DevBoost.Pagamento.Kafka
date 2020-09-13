using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Domain.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class Localizacao
    {
        public Localizacao()
        {

        }
        public Localizacao(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public  double Latitude { get;  set; }
        public  double Longitude { get; set; }

    }
}
