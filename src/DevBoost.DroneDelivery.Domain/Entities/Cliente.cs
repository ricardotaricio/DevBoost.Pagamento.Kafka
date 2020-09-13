using DevBoost.DroneDelivery.Core.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]

    public class Cliente : Entity
    {

        public Cliente(string nome, double latitude, double longitude) : base()
        {
            Nome = nome;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Nome { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

    }
}
