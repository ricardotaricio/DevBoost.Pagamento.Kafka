using DevBoost.DroneDelivery.Core.Domain.Enumerators;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Pagamento.Application.Validations;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.Commands
{
    public class AtualizarSituacaoPagamentoCartaoCommand : Command
    {
        public AtualizarSituacaoPagamentoCartaoCommand(Guid pagamentoId, SituacaoPagamento situacaoPagamneto)
        {
            PagamentoId = pagamentoId;
            SituacaoPagamneto = situacaoPagamneto;
            
        }

        public Guid PagamentoId { get; private set; }
        public SituacaoPagamento SituacaoPagamneto { get; private set; }
       

        public override bool EhValido()
        {
            ValidationResult = new AtualizarSituacaoPagamentoCartaoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
