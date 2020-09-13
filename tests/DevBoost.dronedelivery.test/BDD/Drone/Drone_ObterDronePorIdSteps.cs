using AutoBogus;
using DevBoost.DroneDelivery.API.Controllers;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using DevBoost.DroneDelivery.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using System.Net;
using DevBoost.DroneDelivery.Test.Model;

namespace DevBoost.DroneDelivery.Test.BDD.Drone
{
    [Binding]
    public class Drone_ObterDronePorIdSteps : Cenario
    {
       
        //public Drone_ObterDronePorIdSteps(ScenarioContext context, AutoMocker mocker, AutoFaker faker) : base(context, faker, mocker) { }
        public Drone_ObterDronePorIdSteps(ScenarioContext context) : base(context) { }

        [Given(@"Que eu possua um drone cadastrado")]
        public void DadoQueEuPossuaUmDroneCadastrado()
        {

            var faker = AutoFaker.Create();
            var drone = faker.Generate<Domain.Entities.Drone>();

            _context.Set(drone);
        }

        [Given(@"O usuario esteja logado")]
        public void DadoOUsuarioEstejaLogado()
        {
            var mocker = new AutoMocker();
            var faker = AutoFaker.Create();
            var usuario = faker.Generate<Usuario>();

            var identity = new ClaimsIdentity(new Claim[]
            {  new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
               new Claim(ClaimTypes.Role, usuario.Role.ToString())
            });


            var pedidoControllerMock = mocker.CreateInstance<PedidoController>();
            pedidoControllerMock.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(identity) } };


        }

        [When(@"Eu solicitar um drone por Id")]
        public async Task QuandoEuSolicitarUmDronePorId()
        {

            var mocker = new AutoMocker();
            var faker = AutoFaker.Create();
            var baseControllerMock = mocker.CreateInstance<DroneController>();
            var droneService = mocker.GetMock<IDroneService>();

            droneService.Setup(r => r.GetById(It.IsAny<Int32>())).Returns(Task.FromResult(_context.Get<Domain.Entities.Drone>())).Verifiable();



            //When
            var resut = await baseControllerMock.GetDrone(It.IsAny<Int32>());
            _context.Set(resut.Result);
        }

        [Then(@"O drone será retornado")]
        public void EntaoODroneSeraRetornado()
        {
            //Then
            var result = _context.Get<ActionResult>();

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)Convert.ToInt32(((OkObjectResult)result).StatusCode));

        }
    }
}
