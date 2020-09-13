using AutoBogus;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using DevBoost.DroneDelivery.Infrastructure.Data.Repositories;
using KellermanSoftware.CompareNetObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Xunit;

namespace DevBoost.DroneDelivery.Test.Infrastructure.Data.Repositories
{
    public class ClienteRepositoryTest
    {
        
        [Fact(DisplayName = "ObterTodosClientesComSucesso")]
        [Trait("ClienteRepositoryTest", "Repository Tests")]
        public async void ClienteRepository_ObterTodos_ComSucesso()
        {

            // Given
            var faker = AutoFaker.Create();
            var bus = faker.Generate<IMediatrHandler>();
            var options = new DbContextOptionsBuilder<DCDroneDelivery>()
           .UseInMemoryDatabase(databaseName: "DroneDelivery")
           .Options;

            var clientes = faker.Generate<Cliente>(3);

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                contexto.Cliente.AddRange(clientes);
                await contexto.Commit();
            }

            
            using (var contexto = new DCDroneDelivery(options, bus))
            {
                ClienteRepository clienteRepository = new ClienteRepository(contexto);
                //When
                var cliente = clienteRepository.ObterTodos().Result.ToList();

                //Then
                Assert.True(cliente.Count >0);

            }

        }

        [Fact(DisplayName = "GetById")]
        [Trait("ClienteRepositoryTest", "Repository Tests")]
        public async void GetById_test()
        {

            // Given
            var faker = AutoFaker.Create();
            var bus = faker.Generate<IMediatrHandler>();
            var options = new DbContextOptionsBuilder<DCDroneDelivery>()
           .UseInMemoryDatabase(databaseName: "DroneDelivery")
           .Options;

            var clientes = faker.Generate<Cliente>(3);

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                contexto.Cliente.AddRange(clientes);
                contexto.SaveChanges();
            }

            var expectResponse = clientes.FirstOrDefault();

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                ClienteRepository clienteRepository = new ClienteRepository(contexto);
                //When
                var cliente = await clienteRepository.ObterPorId(expectResponse.Id);

                //Then
                CompareLogic comparer = new CompareLogic();
                Assert.True(comparer.Compare(expectResponse, cliente).AreEqual);

            }

        }





        [Fact(DisplayName = "Update")]
        [Trait("ClienteRepositoryTest", "Repository Tests")]
        public async void Update_test()
        {
            // Given
            var faker = AutoFaker.Create();
            var bus = faker.Generate<IMediatrHandler>();
            var options = new DbContextOptionsBuilder<DCDroneDelivery>().UseInMemoryDatabase(databaseName: "DroneDelivery").Options;
            var cliente = faker.Generate<Cliente>();
            //Seed
            using (var contexto = new DCDroneDelivery(options, bus))
            {
                contexto.Cliente.AddRange(cliente);
                contexto.SaveChanges();
            }

            bool expectResponse;
            bool result;

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                contexto.Cliente.Update(cliente);
                expectResponse = contexto.SaveChanges() > 0;
            }

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                var clienteRepository = new ClienteRepository(contexto);
                await clienteRepository.Atualizar(cliente);
                result = contexto.SaveChanges() > 0;
            }

            var comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);

        }



        [Fact(DisplayName = "Insert")]
        [Trait("ClienteRepositoryTest", "Repository Tests")]
        public async void Insert_test()
        {
            // Given
            var faker = AutoFaker.Create();
            var bus = faker.Generate<IMediatrHandler>();
            var options = new DbContextOptionsBuilder<DCDroneDelivery>()
           .UseInMemoryDatabase(databaseName: "DroneDelivery")
           .Options;

            var clienteNovo = faker.Generate<Cliente>();

            using (var contexto = new DCDroneDelivery(options, bus))
            {
                //when
                var clienteRepository = new ClienteRepository(contexto);
                await clienteRepository.Adicionar(clienteNovo);
                var insertCliente = await clienteRepository.UnitOfWork.Commit();

                //then
                Assert.True(insertCliente);

            }
        }


    }
}
