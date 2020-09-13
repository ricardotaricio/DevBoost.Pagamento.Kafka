using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AdicionarDroneValidation : AbstractValidator<AdicionarDroneCommand>
    {

        public AdicionarDroneValidation()
        {

            RuleFor(d => d.Autonomia)
                .LessThanOrEqualTo(35)
                .WithMessage("A autonomia deve ser menor ou igual a 35 minutos")
                .GreaterThan(0)
                .WithMessage("A autonomia deve ser maior que zero");

            RuleFor(d => d.Capacidade)
                .GreaterThan(0)
                .WithMessage("A capacidade deve ser maior que zero")
            .LessThanOrEqualTo(12)
                .WithMessage("A Capacidade deve ser menor ou igual a 12");

            RuleFor(d => d.Carga)
                .GreaterThan(0)
                .WithMessage("A carga deve ser maior que zero");

            RuleFor(d => d.Velocidade)
                .GreaterThan(0)
                .WithMessage("A velocidade deve ser maior que zero");

        }

    }
}
