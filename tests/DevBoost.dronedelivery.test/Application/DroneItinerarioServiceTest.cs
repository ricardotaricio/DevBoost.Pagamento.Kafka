using AutoBogus;
using DevBoost.DroneDelivery.Application.Services;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
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
    public class DroneItinerarioServiceTest
    {
        [Fact(DisplayName = "GetById")]
        [Trait("DroneItinerarioServiceTest", "Service Tests")]
        public async void GetById_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneItinerarioServiceMock = mocker.CreateInstance<DroneItinerarioService>();

            var faker = AutoFaker.Create();

            var droneItinerario = faker.Generate<DroneItinerario>();

            var responseDroneItinerarioTask = Task.Factory.StartNew(() => droneItinerario);

            var expectResponse = droneItinerario;

            var droneItinerarioRepository = mocker.GetMock<IDroneItinerarioRepository>();

            droneItinerarioRepository.Setup(r => r.ObterPorId(It.IsAny<Int32>())).Returns(responseDroneItinerarioTask).Verifiable();

            //When
            var result = await droneItinerarioServiceMock.GetById(It.IsAny<Int32>());

            //Then
            droneItinerarioRepository.Verify(mock => mock.ObterPorId(It.IsAny<Int32>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "GetAll")]
        [Trait("DroneItinerarioServiceTest", "Service Tests")]
        public async void GetAll_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneItinerarioServiceMock = mocker.CreateInstance<DroneItinerarioService>();

            var faker = AutoFaker.Create();

            var droneItinerarios = faker.Generate<IEnumerable<DroneItinerario>>();

            var responseDroneItinerariosTask = Task.Factory.StartNew(() => droneItinerarios);

            var expectResponse = droneItinerarios;

            var droneItinerarioRepository = mocker.GetMock<IDroneItinerarioRepository>();

            droneItinerarioRepository.Setup(r => r.ObterTodos()).Returns(responseDroneItinerariosTask).Verifiable();

            //When
            var result = await droneItinerarioServiceMock.GetAll();

            //Then
            droneItinerarioRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "GetDroneItinerarioPorIdDrone")]
        [Trait("DroneItinerarioServiceTest", "Service Tests")]
        public async void GetDroneItinerarioPorIdDrone_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneItinerarioServiceMock = mocker.CreateInstance<DroneItinerarioService>();

            var faker = AutoFaker.Create();

            var droneItinerario = faker.Generate<DroneItinerario>();

            var responseDroneItinerarioTask = Task.Factory.StartNew(() => droneItinerario);

            var expectResponse = droneItinerario;

            var droneItinerarioRepository = mocker.GetMock<IDroneItinerarioRepository>();

            droneItinerarioRepository.Setup(r => r.ObterDroneItinerarioPorIdDrone(It.IsAny<Int32>())).Returns(responseDroneItinerarioTask).Verifiable();

            //When
            var result = await droneItinerarioServiceMock.GetDroneItinerarioPorIdDrone(It.IsAny<Int32>());

            //Then
            droneItinerarioRepository.Verify(mock => mock.ObterDroneItinerarioPorIdDrone(It.IsAny<Int32>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Insert")]
        [Trait("DroneItinerarioServiceTest", "Service Tests")]
        public async void Insert_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneItinerarioServiceMock = mocker.CreateInstance<DroneItinerarioService>();

            var faker = AutoFaker.Create();

            var droneItinerario = faker.Generate<DroneItinerario>();

            var responseDroneItinerarioTask = Task.Factory.StartNew(() => true);
            var responseAdicionarDroneItinerarioTask = Task.Factory.StartNew(() => droneItinerario);

            var expectResponse = true;

            var droneItinerarioRepository = mocker.GetMock<IDroneItinerarioRepository>();

            droneItinerarioRepository.Setup(r => r.Adicionar(It.IsAny<DroneItinerario>())).Returns(responseAdicionarDroneItinerarioTask).Verifiable();
            droneItinerarioRepository.Setup(r => r.UnitOfWork.Commit()).Returns(responseDroneItinerarioTask).Verifiable();

            //When
            var result = await droneItinerarioServiceMock.Insert(droneItinerario);

            //Then
            //droneItinerarioRepository.Verify(mock => mock.Adicionar(It.IsAny<DroneItinerario>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }
    }
}
