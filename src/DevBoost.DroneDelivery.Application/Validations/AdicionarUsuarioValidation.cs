using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AdicionarUsuarioValidation : AbstractValidator<AdicionarUsuarioCommand>
    {
        public AdicionarUsuarioValidation()
        {
            RuleFor(c => c.UserName)
              .NotEmpty()
              .WithMessage("Nome é necessário");

            RuleFor(c => c.Password)
             .NotEmpty()
             .WithMessage("Senha é necessária");
        }
    }
}
