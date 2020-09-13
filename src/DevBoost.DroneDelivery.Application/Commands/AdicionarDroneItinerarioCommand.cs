using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarDroneItinerarioCommand : Command
    {
        public AdicionarDroneItinerarioCommand(DateTime dataHora, Guid droneId, EnumStatusDrone statusDrone)
        {
            DataHora = dataHora;
            DroneId = droneId;
            StatusDrone = statusDrone;
        }

        public DateTime DataHora { get; private set; }
        public Guid DroneId { get; private set; }
        public EnumStatusDrone StatusDrone { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarDroneItinerarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
