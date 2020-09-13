using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace DevBoost.DroneDelivery.API.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IUsuarioQueries  _usuarioQueries;
        
        public OAuthController(IUsuarioQueries usuarioQueries)
        {
            _usuarioQueries = usuarioQueries;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate([FromBody] LoginViewModel  loginViewModel)
        {
            var user = await _usuarioQueries.ObterPorCredenciais(loginViewModel.Nome, loginViewModel.Senha);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            return Ok(TokenGenerator.GenerateToken(user));
        }
    }
}
