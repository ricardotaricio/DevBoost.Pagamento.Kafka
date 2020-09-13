using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
    {

        public AdicionarPedidoValidation()
        {
            RuleFor(p => p.Peso)
                .GreaterThan(0)
                .WithMessage("Peso do pedido deve ser maior que zero");

        }

    }
}
