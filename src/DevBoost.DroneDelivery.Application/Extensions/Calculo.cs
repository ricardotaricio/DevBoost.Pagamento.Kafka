using DevBoost.DroneDelivery.Domain.ValueObjects;
using System;
using System.Device.Location;

namespace DevBoost.DroneDelivery.Application.Extensions
{
    public static class Calculo
    {
        public static double CalcularDistanciaEmKilometros(this Localizacao origem, Localizacao destino)
        {
            var origemCoord = new GeoCoordinate(origem.Latitude, origem.Longitude);
            var destinoCoord = new GeoCoordinate(destino.Latitude, destino.Longitude);

            var distance = origemCoord.GetDistanceTo(destinoCoord);

            distance /= 1000;

            return distance;
        }

        public static int CalcularTempoTrajetoEmMinutos(this double distanciaEmKilometros, int velocidadeEmKilometrosPorHora)
        {
            double tempo = distanciaEmKilometros / velocidadeEmKilometrosPorHora;

            tempo *= 60;

            return Convert.ToInt32(Math.Ceiling(tempo));
        }
    }
}
