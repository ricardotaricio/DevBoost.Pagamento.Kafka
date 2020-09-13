using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarClienteCommand : Command
    {
        

        public AdicionarClienteCommand(string nome, string usuario, string senha, double latitude, double longitude)
        {
            Nome = nome;
            Usuario = usuario;
            Senha = senha;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Nome { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private  set; }



        public override bool EhValido()
        {
            ValidationResult = new AdicionarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
