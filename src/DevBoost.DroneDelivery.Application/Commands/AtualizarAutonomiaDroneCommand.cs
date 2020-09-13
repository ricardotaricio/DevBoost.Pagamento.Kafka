using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AtualizarAutonomiaDroneCommand : Command
    {
        public AtualizarAutonomiaDroneCommand(Guid droneId, int autonomiaRestante)
        {
            DroneId = droneId;
            AutonomiaRestante = autonomiaRestante;
        }

        public Guid DroneId { get; private set; }
        public int AutonomiaRestante { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarAutonomiaDroneValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
