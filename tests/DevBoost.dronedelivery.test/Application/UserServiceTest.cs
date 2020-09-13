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
    public class UserServiceTest
    {
        [Fact(DisplayName = "GetByUserName")]
        [Trait("UserServiceTest", "Service Tests")]
        public async void GetByUserName_test()
        {
            // Given
            var mocker = new AutoMocker();
            var userServiceMock = mocker.CreateInstance<UserService>();

            var faker = AutoFaker.Create();

            var user = faker.Generate<Usuario>();

            var responseUserTask = Task.Factory.StartNew(() => user);

            var expectResponse = user;

            var userRepository = mocker.GetMock<IUserRepository>();

            userRepository.Setup(r => r.ObterPorNome(It.IsAny<string>())).Returns(responseUserTask).Verifiable();

            //When
            var result = await userServiceMock.GetByUserName(It.IsAny<string>());

            //Then
            userRepository.Verify(mock => mock.ObterPorNome(It.IsAny<string>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Authenticate")]
        [Trait("UserServiceTest", "Service Tests")]
        public async void GetAll_test()
        {
            // Given
            var mocker = new AutoMocker();
            var userServiceMock = mocker.CreateInstance<UserService>();

            var faker = AutoFaker.Create();

            var user = faker.Generate<Usuario>();

            var responseUserTask = Task.Factory.StartNew(() => user);

            var expectResponse = user;

            var userRepository = mocker.GetMock<IUserRepository>();

            userRepository.Setup(r => r.ObterCredenciais(It.IsAny<string>(), It.IsAny<string>())).Returns(responseUserTask).Verifiable();

            //When
            var result = await userServiceMock.Authenticate(It.IsAny<string>(), It.IsAny<string>());

            //Then
            userRepository.Verify(mock => mock.ObterCredenciais(It.IsAny<string>(), It.IsAny<string>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }

        [Fact(DisplayName = "Insert")]
        [Trait("UserServiceTest", "Service Tests")]
        public async void Insert_test()
        {
            // Given
            var mocker = new AutoMocker();
            var userServiceMock = mocker.CreateInstance<UserService>();

            var faker = AutoFaker.Create();

            var user = faker.Generate<Usuario>();

            var responseUserTask =  true;

            var expectResponse = true;

            var userRepository = mocker.GetMock<IUserRepository>();

            userRepository.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(responseUserTask).Verifiable();

            //When
            var result = await userServiceMock.Insert(user);

            //Then
            userRepository.Verify(mock => mock.Adicionar(It.IsAny<Usuario>()), Times.Once());

            CompareLogic comparer = new CompareLogic();
            Assert.True(comparer.Compare(expectResponse, result).AreEqual);
        }
    }
}
