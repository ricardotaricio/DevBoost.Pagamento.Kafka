using AutoBogus;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DevBoost.DroneDelivery.Test.API
{
    public class ClienteControllerTest
    {
        [Fact(DisplayName = "ObterClientePorIdComSucesso")]
        [Trait("ClienteControllerTest", "Controller Tests")]
        public void Cliente_ObterPorId_ComSucesso()
        {
            // Given
            var mocker = new AutoMocker();
            var baseControllerMock = mocker.CreateInstance<ClienteController>();

            var faker = AutoFaker.Create();

            var response = faker.Generate<Cliente>();

            var responseTask = Task.Factory.StartNew(() => response);

            var expectResponse = baseControllerMock.Ok(response);

            var clienteService = mocker.GetMock<IClienteService>();

            clienteService.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(responseTask).Verifiable();

            //When
            var result =  baseControllerMock.Get(It.IsAny<Guid>()).Result;

            //Then

            clienteService.Verify(mock => mock.GetById(It.IsAny<Guid>()), Times.Once());
            Assert.Equal((HttpStatusCode)expectResponse.StatusCode, (HttpStatusCode)Convert.ToInt32(((OkObjectResult)result).StatusCode));
        }
        
        [Fact(DisplayName = "ObterTodosClientesComSucesso")]
        [Trait("ClienteControllerTest", "Controller Tests")]
        public void Cliente_ObterTodos_ComSucesso()
        {
            // Given
            var mocker = new AutoMocker();
            var faker = AutoFaker.Create();
            var baseControllerMock = mocker.CreateInstance<ClienteController>();


            var response = faker.Generate<Cliente>(5);

            var responseTask = response;

            var expectResponse = baseControllerMock.Ok(response);

            var clienteService = mocker.GetMock<IClienteService>();

            clienteService.Setup(r => r.GetAll()).ReturnsAsync(responseTask).Verifiable();

            //When
            var result = baseControllerMock.Get().Result;

            //Then

            clienteService.Verify(mock => mock.GetAll(), Times.Once());
            Assert.Equal((HttpStatusCode)expectResponse.StatusCode, (HttpStatusCode)Convert.ToInt32(((OkObjectResult)result).StatusCode));
        }
        [Fact(DisplayName = "AdicionarPedidoComSucesso")]
        [Trait("PedidoControllerTest", "Controller Tests")]
        public async Task Pedido_AdicionarPedido_ComSucessoAsync()
        {
            // Given
            var mocker = new AutoMocker();
            var faker = AutoFaker.Create();
            var adicionarPedidoViewModel = faker.Generate<AdicionarClienteViewModel>();
            var usuario = faker.Generate<Usuario>();
            var cliente = faker.Generate<Cliente>();
            var identity = new ClaimsIdentity(new Claim[]
            {  new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
               new Claim(ClaimTypes.Role, usuario.Role.ToString())
            });


            var clienteControllerMock = mocker.CreateInstance<ClienteController>();
            clienteControllerMock.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(identity) } };
                        
            var responeInserirClienteTask = Task.Factory.StartNew(() => true);
            var clienteService = mocker.GetMock<IClienteService>();
            var usuarioService = mocker.GetMock<IUserService>();

            clienteService.Setup(p => p.Insert(It.IsAny<Cliente>())).Returns(responeInserirClienteTask).Verifiable();
            usuarioService.Setup(u => u.Insert(It.IsAny<Usuario>())).Returns(responeInserirClienteTask).Verifiable();


            var expectResponse = clienteControllerMock.CreatedAtAction("Get", new { id = cliente.Id }, cliente);

            //When
            var result = await clienteControllerMock.Post(adicionarPedidoViewModel);

            //Then
            
            clienteService.Verify(p => p.Insert(It.IsAny<Cliente>()), Times.Once());

            Assert.Equal((HttpStatusCode)expectResponse.StatusCode, (HttpStatusCode)Convert.ToInt32(((CreatedAtActionResult)result).StatusCode));
        }
    }
}
