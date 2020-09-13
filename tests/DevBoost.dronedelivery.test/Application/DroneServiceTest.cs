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
    public class DroneServiceTest
    {
        [Fact(DisplayName = "GetById")]
        [Trait("DroneServiceTest", "Service Tests")]
        public async void GetById_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneServiceMock = mocker.CreateInstance<DroneService>();

            var faker = AutoFaker.Create();

            var drone = faker.Generate<Drone>();

            var responseDroneTask = Task.Factory.StartNew(() => drone);

            var expectResponse = drone;

            var droneRepository = mocker.GetMock<IDroneRepository>();

            droneRepository.Setup(r => r.ObterPorId(It.IsAny<Int32>())).Returns(responseDroneTask).Verifiable();

            //When
            var result = await droneServiceMock.GetById(It.IsAny<Int32>());

            //Then
            droneRepository.Verify(mock => mock.ObterPorId(It.IsAny<Int32>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "GetAll")]
        [Trait("DroneServiceTest", "Service Tests")]
        public async void GetAll_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneServiceMock = mocker.CreateInstance<DroneService>();

            var faker = AutoFaker.Create();

            var drones = faker.Generate<IEnumerable<Drone>>();

            var responseDronesTask = Task.Factory.StartNew(() => drones);

            var expectResponse = drones;

            var droneRepository = mocker.GetMock<IDroneRepository>();

            droneRepository.Setup(r => r.ObterTodos()).Returns(responseDronesTask).Verifiable();

            //When
            var result = await droneServiceMock.GetAll();

            //Then
            droneRepository.Verify(mock => mock.ObterTodos(), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Insert")]
        [Trait("DroneServiceTest", "Service Tests")]
        public async void Insert_test()
        {
            // Given
            var mocker = new AutoMocker();
            var droneServiceMock = mocker.CreateInstance<DroneService>();

            var faker = AutoFaker.Create();

            var drone = faker.Generate<Drone>();

            var responseDroneTask = Task.Factory.StartNew(() => true);
            var responseAdicionarDroneTask = Task.Factory.StartNew(() => drone);

            var expectResponse = true;

            var droneRepository = mocker.GetMock<IDroneRepository>();

            droneRepository.Setup(r => r.Adicionar(It.IsAny<Drone>())).Returns(responseAdicionarDroneTask).Verifiable();
            droneRepository.Setup(r => r.UnitOfWork.Commit()).Returns(responseDroneTask).Verifiable();

            //When
            var result = await droneServiceMock.Insert(drone);

            //Then
           // droneRepository.Verify(mock => mock.Adicionar(It.IsAny<Drone>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }
    }
}
