using AutoBogus;
using DevBoost.DroneDelivery.API.Controllers;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Test.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace DevBoost.DroneDelivery.Test.BDD.Drone
{
    [Binding]
    public class Drone_AdicionarUmNovoDroneSteps: Cenario
    {
        
        //public Drone_AdicionarUmNovoDroneSteps(ScenarioContext context, AutoMocker mocker, AutoFaker faker) : base(context, faker, mocker) { }
        public Drone_AdicionarUmNovoDroneSteps(ScenarioContext context) : base(context) { }

        [Given(@"Que eu possua um drone")]
        public void DadoQueEuPossuaUmDrone()
        {
            var faker = AutoFaker.Create();
            var drone = faker.Generate<Domain.Entities.Drone>();

            _context.Set(drone);
        }
        
        [Given(@"O Usuario esteja logado")]
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
        
        [When(@"O usuario adicionar um drone")]
        public async Task QuandoOUsuarioAdicionarUmDroneAsync()
        {
            var mocker = new AutoMocker();
            var faker = AutoFaker.Create();
            var baseControllerMock = mocker.CreateInstance<DroneController>();
            var droneService = mocker.GetMock<IDroneService>();
            var adicionarDroneViewModel = faker.Generate<AdicionarDroneViewModel>();
            droneService.Setup(r => r.Insert(It.IsAny<Domain.Entities.Drone>())).Returns(Task.FromResult(true)).Verifiable();

            //When
            var resut = await baseControllerMock.PostDrone(adicionarDroneViewModel);
            _context.Set(resut);
        }

        [Then(@"Recebe uma confirmação")]
        public void EntaoRecebeUmaConfirmacao()
        {
            //Then
            var result = _context.Get<ActionResult>();

            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)Convert.ToInt32(((CreatedAtActionResult)result).StatusCode));
        }

    }
}
