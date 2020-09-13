using DevBoost.DroneDelivery.API;
using DevBoost.DroneDelivery.Features.Test.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace DevBoost.DroneDelivery.Features.Test.Features
{
    [CollectionDefinition(nameof(IntegrationTestsFixture))]
    public class IntegrationTestsFixture : ICollectionFixture<IntegrationTestsFixture<Startup>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public readonly DroneDeliveryAppFactory<TStartup> Factory;
        public HttpClient Client;

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

        public void Dispose()
        {
            Client?.Dispose();
            Factory?.Dispose();
        }
    }



}
