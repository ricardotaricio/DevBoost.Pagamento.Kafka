using AutoBogus;
using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.API
{
    public class DroneControllerTest
    {
        [Fact(DisplayName = "GetById")]
        [Trait("DroneControllerTest", "Controller Tests")]
        public async void GetById_test()
        {
            // Given
            var mocker = new AutoMocker();
            var baseControllerMock = mocker.CreateInstance<DroneController>();

            var faker = AutoFaker.Create();

            var response = faker.Generate<Drone>();

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok(response);

            var droneService = mocker.GetMock<IDroneService>();

            droneService.Setup(r => r.GetById(It.IsAny<Int32>())).Returns(responseTask).Verifiable();

            //When
            var result = await baseControllerMock.GetDrone(It.IsAny<Int32>());

            //Then
            
            droneService.Verify(mock => mock.GetById(It.IsAny<Int32>()), Times.Once());
            Assert.Equal((HttpStatusCode)expectResponse.StatusCode, (HttpStatusCode)Convert.ToInt32(((OkObjectResult)result.Result).StatusCode));
        }

    }
}
