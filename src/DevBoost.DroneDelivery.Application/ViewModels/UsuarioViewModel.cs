using System;

namespace DevBoost.DroneDelivery.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid ClienteId { get; set; }
    }
}
