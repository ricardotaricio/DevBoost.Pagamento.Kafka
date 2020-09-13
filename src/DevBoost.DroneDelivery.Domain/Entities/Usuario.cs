using DevBoost.DroneDelivery.Core.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Usuario : Entity
    {
        public Usuario()
        {

        }

        public Usuario(string username, string password, string role,Guid ClienteId)
        {
            
            UserName = username;
            Password = password;
            Role = role;
        }

        
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
        public Cliente Cliente { get; set; }

    }
}
