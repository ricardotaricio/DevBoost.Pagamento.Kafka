using DevBoost.DroneDelivery.Domain.ValueObjects;
using System;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Peso { get; set; }
        public Cartao Cartao { get; set; }
        public double Valor { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public DroneViewModel Drone { get; set; }
        public string Situacao { get; set; }
    }
}
