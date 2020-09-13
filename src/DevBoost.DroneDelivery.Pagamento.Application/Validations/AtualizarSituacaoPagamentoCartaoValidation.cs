using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using FluentValidation;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.Validations
{
    public class AtualizarSituacaoPagamentoCartaoValidation : AbstractValidator<AtualizarSituacaoPagamentoCartaoCommand>
    {
        public AtualizarSituacaoPagamentoCartaoValidation()
        {
            RuleFor(p => p.PagamentoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Pagamento deve ser informado");
        }
    }
}
