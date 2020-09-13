using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarDroneCommand : Command
    {
        public AdicionarDroneCommand(int capacidade, int velocidade, int autonomia)
        {
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            Carga = 60;
            AutonomiaRestante = autonomia;
            StatusDrone = EnumStatusDrone.Disponivel;
            DataHora = DateTime.Now;

        }

        public int Capacidade { get; private set; }
        public int Velocidade { get; private set; }
        public int Autonomia { get; private set; }
        public int Carga { get; private set; }
        public int AutonomiaRestante { get; private set; }
        public EnumStatusDrone StatusDrone { get; private set; }
        public DateTime DataHora { get; private set; }
        
        public override bool EhValido()
        {
            ValidationResult = new AdicionarDroneValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
