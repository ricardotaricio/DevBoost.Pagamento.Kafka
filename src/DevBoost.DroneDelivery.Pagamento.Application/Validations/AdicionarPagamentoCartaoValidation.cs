using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using FluentValidation;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.Validations
{
    public class AdicionarPagamentoCartaoValidation : AbstractValidator<AdicionarPagamentoCartaoCommand>
    {
        public AdicionarPagamentoCartaoValidation()
        {
            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage("Valor do pagamento deve ser maior que zero.");

            RuleFor(p => p.NumeroCartao)
                .CreditCard()
                .WithMessage("Numero do cartão inválido.");

            RuleFor(p => p.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Pedido deve ser informado");
        }
    }
}
