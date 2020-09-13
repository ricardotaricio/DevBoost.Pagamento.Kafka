using AutoBogus;
using DevBoost.DroneDelivery.Application.Services;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.Application
{
    public class ClienteServiceTest
    {
        [Fact(DisplayName = "GetById")]
        [Trait("ClienteServiceTest", "Service Tests")]
        public async void GetById_test()
        {
            // Given
            var mocker = new AutoMocker();
            var clienteServiceMock = mocker.CreateInstance<ClienteService>();

            var faker = AutoFaker.Create();

            var cliente = faker.Generate<Cliente>();

            var responseClienteTask = Task.Factory.StartNew(() => cliente);

            var expectResponse = cliente;

            var clienteRepository = mocker.GetMock<IClienteRepository>();

            clienteRepository.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(responseClienteTask).Verifiable();

            //When
            var result = await clienteServiceMock.GetById(It.IsAny<Guid>());

            //Then

            clienteRepository.Verify(mock => mock.ObterPorId(It.IsAny<Guid>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "GetAll")]
        [Trait("ClienteServiceTest", "Service Tests")]
        public async void GetAll_test()
        {
            // Given
            var mocker = new AutoMocker();
            var clienteServiceMock = mocker.CreateInstance<ClienteService>();

            var faker = AutoFaker.Create();

            var clientes = faker.Generate<IEnumerable<Cliente>>();

            var responseClientesTask = Task.Factory.StartNew(() => clientes);

            var expectResponse = clientes;

            var clienteRepository = mocker.GetMock<IClienteRepository>();

            clienteRepository.Setup(r => r.ObterTodos()).Returns(responseClientesTask).Verifiable();

            //When
            var result = await clienteServiceMock.GetAll();

            //Then
            clienteRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Insert")]
        [Trait("ClienteServiceTest", "Service Tests")]
        public async void Insert_test()
        {
            // Given
            var mocker = new AutoMocker();
            var clienteServiceMock = mocker.CreateInstance<ClienteService>();

            var faker = AutoFaker.Create();

            var cliente = faker.Generate<Cliente>();

            var responseClienteTask = Task.Factory.StartNew(() => true);
            var responseAdicionarClienteTask = Task.Factory.StartNew(() => cliente);
            
            var expectResponse = true;

            var clienteRepository = mocker.GetMock<IClienteRepository>();

            clienteRepository.Setup(r => r.Adicionar(It.IsAny<Cliente>())).Returns(responseAdicionarClienteTask).Verifiable();
            clienteRepository.Setup(r => r.UnitOfWork.Commit()).Returns(responseClienteTask).Verifiable();

            //When
            var result = await clienteServiceMock.Insert(cliente);

            //Then
            //clienteRepository.Verify(mock => mock.Adicionar(It.IsAny<Cliente>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }
    }
}
