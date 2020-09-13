namespace DevBoost.DroneDelivery.Domain.ValueObjects
{
    public struct Cartao
    {
        public string Numero { get; set; }
        public string Bandeira { get; set; }
        public short MesVencimento { get; set; }
        public short AnoVencimento { get; set; }
    }
}
