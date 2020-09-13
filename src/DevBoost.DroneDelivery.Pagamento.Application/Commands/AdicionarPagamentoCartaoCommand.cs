using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Pagamento.Application.Validations;
using System;

namespace DevBoost.DroneDelivery.Pagamento.Application.Commands
{
    public class AdicionarPagamentoCartaoCommand : Command
    {
        public AdicionarPagamentoCartaoCommand(Guid pedidoId, double valor, string bandeiraCartao, string numeroCartao, int mesVencimentoCartao, int anoVencimentoCartao)
        {
            PedidoId = pedidoId;
            Valor = valor;
            BandeiraCartao = bandeiraCartao;
            NumeroCartao = numeroCartao;
            MesVencimentoCartao = mesVencimentoCartao;
            AnoVencimentoCartao = anoVencimentoCartao;
        }

        public Guid PedidoId { get; private set; }
        public double Valor { get; private set; }
        public string BandeiraCartao { get;private set; }
        public string NumeroCartao { get; private set; }
        public int MesVencimentoCartao { get; private set; }
        public int AnoVencimentoCartao { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarPagamentoCartaoValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
