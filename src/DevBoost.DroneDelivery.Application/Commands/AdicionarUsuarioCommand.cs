using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarUsuarioCommand : Command
    {
        public AdicionarUsuarioCommand(string userName, string password,Guid clienteId, string role)
        {
            UserName = userName;
            Password = password;
            Role = role;
            ClienteId = clienteId;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public Guid  ClienteId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
