using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Pedido : Entity
    {
        public Pedido(int peso, DateTime dataHora,EnumStatusPedido status, double valor)
        {
            Peso = peso;
            DataHora = dataHora;
            Status = status;
            Valor = valor;
        }

        public int Peso { get; set; }
        public double Valor { get; set; }
        public DateTime DataHora { get; private set; }
        public Drone Drone { get; set; }
        public EnumStatusPedido Status { get; private set; }
        public Cliente Cliente { get; private set; }


        public void InformarHoraPedido(DateTime horaPedido)
        {
            this.DataHora = horaPedido;
        }

        public void AtualizarStatus(EnumStatusPedido statusPedido)
        {
            Status = statusPedido;
        }

        public void InformarDrone(Drone drone)
        {
            Drone = drone;
        }

        public void InformarStatus(EnumStatusPedido status)
        {
            Status = status;
        }

        public void InformarCliente(Cliente cliente)
        {
            Cliente = cliente;
        }

        public void InformarPeso(int peso)
        {
            Peso = peso;
        }
    }
}
