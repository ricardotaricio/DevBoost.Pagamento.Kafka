using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AdicionarDroneItinerarioValidation : AbstractValidator<AdicionarDroneItinerarioCommand>
    {
        public AdicionarDroneItinerarioValidation()
        {
            RuleFor(p => p.DroneId)
              .NotEmpty()
                .WithMessage("Drone deve ser informado!");
        }
    }
}
