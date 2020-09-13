using AutoBogus;
using DevBoost.DroneDelivery.Application.Services;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.Application
{
    public class PedidoServiceTest
    {
        [Fact(DisplayName = "GetById")]
        [Trait("PedidoServiceTest", "Service Tests")]
        public async void GetById_test()
        {
            // Given
            var mocker = new AutoMocker();
            var pedidoServiceMock = mocker.CreateInstance<PedidoService>();

            var faker = AutoFaker.Create();

            var drone = faker.Generate<Drone>();

            // var pedido = faker.Generate<Pedido>();
            var pedidoFaker = new AutoFaker<Pedido>()
                .RuleFor(u => u.DroneId, f => f.Random.Int(1));

            var pedido = pedidoFaker.Generate();

            var responseDroneTask = Task.Factory.StartNew(() => drone);
            var responsePedidoTask = Task.Factory.StartNew(() => pedido);

            var expectResponse = pedido; // pedidoServiceMock.GetById(responsePedido);

            var droneRepository = mocker.GetMock<IDroneRepository>();
            var pedidoRepository = mocker.GetMock<IPedidoRepository>();

            droneRepository.Setup(r => r.ObterPorId(It.IsAny<Int32>())).Returns(responseDroneTask).Verifiable();
            pedidoRepository.Setup(r => r.ObterPorId(It.IsAny<Guid>())).Returns(responsePedidoTask).Verifiable();

            //When
            var result = await pedidoServiceMock.GetById(It.IsAny<Guid>());

            //Then

            droneRepository.Verify(mock => mock.ObterPorId(It.IsAny<Int32>()), Times.Once());
            pedidoRepository.Verify(mock => mock.ObterPorId(It.IsAny<Guid>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "GetAll")]
        [Trait("PedidoServiceTest", "Service Tests")]
        public async void GetAll_test()
        {
            // Given
            var mocker = new AutoMocker();
            var pedidoServiceMock = mocker.CreateInstance<PedidoService>();

            var faker = AutoFaker.Create();

            var pedidos = faker.Generate<IEnumerable<Pedido>>();

            var responsePedidoTask = Task.Factory.StartNew(() => pedidos);

            var expectResponse = pedidos;

            var pedidoRepository = mocker.GetMock<IPedidoRepository>();

            pedidoRepository.Setup(r => r.ObterTodos()).Returns(responsePedidoTask).Verifiable();

            //When
            var result = await pedidoServiceMock.GetAll();

            //Then
            pedidoRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Insert")]
        [Trait("PedidoServiceTest", "Service Tests")]
        public async void Pedido_InsertPedidoDentroDaCapacidadeDoDrone_Sucesso()
        {
            // Given
            var mocker = new AutoMocker();
            var pedidoServiceMock = mocker.CreateInstance<PedidoService>();

            var faker = AutoFaker.Create();

            var pedidoFaker = new AutoFaker<Pedido>()
                .RuleFor(u => u.Peso, f => 10);
            var pedido = pedidoFaker.Generate();

            var drones = new AutoFaker<Drone>()
                .RuleFor(u => u.Capacidade, f => f.Random.Int(7, 12)).Generate(3);

            var responsePedidoTask = Task.Factory.StartNew(() => true);
            var responseDronesTask = drones;

            var expectResponse = true;

            var pedidoRepository = mocker.GetMock<IPedidoRepository>();
            var droneRepository = mocker.GetMock<IDroneRepository>();

            pedidoRepository.Setup(r => r.Adicionar(It.IsAny<Pedido>())).Returns(responsePedidoTask).Verifiable();
            pedidoRepository.Setup(r => r.UnitOfWork.Commit()).Returns(responsePedidoTask).Verifiable();
            droneRepository.Setup(r => r.ObterTodos()).ReturnsAsync(responseDronesTask).Verifiable();

            //When
            var result = await pedidoServiceMock.Insert(pedido);

            //Then
            //pedidoRepository.Verify(mock => mock.Adicionar(It.IsAny<Pedido>()), Times.Once());
            //droneRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "VerificarIsPedidoValidoComSucesso")]
        [Trait("PedidoServiceTest", "Service Tests")]
        public void Pedido_IsPedidoValido_Sucesso()
        {
            // Given
            var mocker = new AutoMocker();
            var pedidoServiceMock = mocker.CreateInstance<PedidoService>();

            var faker = AutoFaker.Create();

            var drones = new AutoFaker<Drone>()
                .RuleFor(u => u.Capacidade, f => f.Random.Int(7, 12))
                .RuleFor(u => u.Velocidade, 40)
                .RuleFor(u => u.Autonomia, 40)
                .Generate(3);

            var cliente = new AutoFaker<Cliente>()
                .RuleFor(c => c.Latitude, -23.594987)
                .RuleFor(c => c.Longitude, -46.6552518).Generate();

            // var pedido = faker.Generate<Pedido>();
            var pedido = new AutoFaker<Pedido>()
                .RuleFor(u => u.Peso, 2)
                .RuleFor(u => u.Cliente, cliente).Generate();

            var responseDroneTask = drones;
            var responsePedidoTask = Task.Factory.StartNew(() => pedido);

            var expectResponse = string.Empty;

            var droneRepository = mocker.GetMock<IDroneRepository>();

            droneRepository.Setup(r => r.ObterTodos()).ReturnsAsync(responseDroneTask).Verifiable();

            //When
            var result = pedidoServiceMock.IsPedidoValido(pedido);

            //Then
            droneRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }
    }
}
