using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DevBoost.DroneDelivery.Pagamento.Domain.Entites
{
    [ExcludeFromCodeCoverage]
    public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(Guid id, string username, string password, string role)
        {
            Id = id;
            UserName = username;
            Password = password;
            Role = role;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
