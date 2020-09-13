using DevBoost.DroneDelivery.Application.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevBoost.DroneDelivery.Infrastructure.Security
{
    public static class TokenGenerator
    {
        public static Token TokenConfig { get; set; }

        public static string GenerateToken(UsuarioViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = TokenConfig.ValidoEm,
                Issuer = TokenConfig.Emissor,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(TokenConfig.ExpiracaoEmMinutos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenConfig.ObterChave()), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
