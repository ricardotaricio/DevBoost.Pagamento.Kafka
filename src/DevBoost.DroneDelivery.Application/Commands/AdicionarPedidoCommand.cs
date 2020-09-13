using DevBoost.Dronedelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarPedidoCommand : Command
    {
        public AdicionarPedidoCommand(Guid clienteId, double valor, int peso, DateTime dataHora,string bandeiraCartao, string numeroCartao, short mesVencimentoCartao, short anoVencimentoCartao)
        {
            ClienteId = clienteId;
            Valor = valor;
            Peso = peso;
            DataHora = dataHora;
            Status = EnumStatusPedido.AguardandoPagamento;
            BandeiraCartao = bandeiraCartao;
            NumeroCartao = numeroCartao;
            MesVencimentoCartao = mesVencimentoCartao;
            AnoVencimentoCartao = anoVencimentoCartao;
        }

        
        

        public Guid ClienteId { get; set; }
        public double Valor { get; set; }
        public int Peso { get; private set; }
        public DateTime DataHora { get; private set; }
        public EnumStatusPedido Status { get; private set; }
        public string BandeiraCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public short MesVencimentoCartao { get; private set; }
        public short AnoVencimentoCartao { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }




}
