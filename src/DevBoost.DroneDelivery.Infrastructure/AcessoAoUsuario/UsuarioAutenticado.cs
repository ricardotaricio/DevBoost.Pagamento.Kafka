using DevBoost.DroneDelivery.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace DevBoost.DroneDelivery.Infrastructure.AcessoAoUsuario
{
    public class UsuarioAutenticado : IUsuarioAutenticado
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioAutenticado(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public String GetCurrentUserName()
        {
            var usuarioUserName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.Name)?.Value;

            return usuarioUserName;
        }
    }
}
