using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AtualizarAutonomiaDroneValidation : AbstractValidator<AtualizarAutonomiaDroneCommand>
    {
        public AtualizarAutonomiaDroneValidation()
        {
            RuleFor(d => d.AutonomiaRestante)
               .GreaterThan(0)
               .WithMessage("Autonomia restante deve ser maior que zero");
        }

       

    }
}
