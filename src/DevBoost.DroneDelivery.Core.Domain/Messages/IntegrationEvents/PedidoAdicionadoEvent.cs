using System;

namespace DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents
{
    public class PedidoAdicionadoEvent : Event
    {

        public PedidoAdicionadoEvent(Guid entityId, double valor, string bandeiraCartao, string numeroCartao, short mesVencimentoCartao, short anoVencimentoCartao) : base(entityId)
        {
            Valor = valor;
            BandeiraCartao = bandeiraCartao;
            NumeroCartao = numeroCartao;
            MesVencimentoCartao = mesVencimentoCartao;
            AnoVencimentoCartao = anoVencimentoCartao;
        }

        public double Valor { get; private set; }
        public string BandeiraCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public short MesVencimentoCartao { get; private set; }
        public short AnoVencimentoCartao { get; private set; }

    }
}
