using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.Config
{

    [CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]

    public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly DroneDeliveryAppFactory<TStartup> Factory;
        public HttpClient Client;
        public string UsuarioNome;
        public string UsuarioSenha;
        public string UsuarioToken;
        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost")
                
            };

            Factory = new DroneDeliveryAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }
        

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioNome = faker.Internet.UserName().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }
        public async Task RealizarLoginApi()
        {
            var userData = new LoginViewModel
            {
                Nome = "Mirosmar",
                Senha = "Teste@123"
            };

            Client = Factory.CreateClient();

            var response = await Client.PostAsync("/login", new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }
        public void Dispose()
        {
            Client?.Dispose();
            Factory?.Dispose();
        }
    }
}
