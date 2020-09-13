using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Domain.ValueObjects;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class DespacharPedidoCommand : Command
    {
        public DespacharPedidoCommand()
        {
            
            LocalizacaoLoja = new Localizacao(-23.5880684, -46.6564195);
        }

        public Localizacao LocalizacaoLoja { get; set; }
       
        public override bool EhValido()
        {
            ValidationResult = new DespacharPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
