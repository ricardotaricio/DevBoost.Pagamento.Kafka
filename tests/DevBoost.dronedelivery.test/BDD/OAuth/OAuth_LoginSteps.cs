using AutoBogus;
using DevBoost.DroneDelivery.API.Controllers;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Test.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace DevBoost.DroneDelivery.Test.BDD.OAuth
{
    [Binding]
    public class OAuth_LoginSteps : Cenario
    {

        public OAuth_LoginSteps(ScenarioContext context) : base( context) { }
       

        [Given(@"Que o cliente possua um usuário cadastrado")]
        public void DadoQueOClientePossuaUmUsuarioCadastrado()
        {
            var faker = AutoFaker.Create();
            var user = faker.Generate<Usuario>();


            _context.Set(user);
        }

        [When(@"o cliente informa seu nome de usuario correto")]
        public void QuandoOClienteInformaSeuNomeDeUsuarioCorreto()
        {
            var faker = AutoFaker.Create();
            var username = faker.Generate<string>();

            _context.Set(username);
        }

        [When(@"o cliente informa sua senha correta")]
        public void QuandoOClienteInformaSuaSenhaCorreta()
        {
            var faker = AutoFaker.Create();
            var password = faker.Generate<string>();

            var mocker = new AutoMocker();
            var baseControllerMock = mocker.CreateInstance<OAuthController>();
            var userService = mocker.GetMock<IUserService>();
            var loginViewModel = faker.Generate<LoginViewModel>();

            userService.Setup(r => r.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_context.Get<Usuario>())).Verifiable();

            //When
            var resut = baseControllerMock.Authenticate(loginViewModel);

            _context.Set(password);
            _context.Set(resut.Result);
        }
        
        [Then(@"o cliente é autenticado")]
        public void EntaoOClienteEAutenticado()
        {
            var result = _context.Get<ActionResult>();

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)Convert.ToInt32(((OkObjectResult)result).StatusCode));
        }
    }
}
