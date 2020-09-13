using DevBoost.DroneDelivery.API;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Test.Config;
using DevBoost.DroneDelivery.Test.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.API.Integracao
{


    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class PedidoTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        public PedidoTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar novo pedido")]
        [Trait("Pedido", "Integração API - Pedido")]
        public async Task Adicionar_NovoPedido_DeveRetornarComSucesso()
        {
            // Arrange
            var pedidoInfo = new AdicionarPedidoViewModel {Peso=10};

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsync("api/pedido", new StringContent(JsonConvert.SerializeObject(pedidoInfo), Encoding.UTF8, "application/json") );

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
