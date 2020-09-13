using AutoBogus;
using AutoMapper;
using DevBoost.DroneDelivery.Pagamento.Application.Commands;
using DevBoost.DroneDelivery.Pagamento.Application.ViewModels;
using DevBoost.DroneDelivery.Test.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace DevBoost.DroneDelivery.Pagamento.API.Tests.BDD
{
    [Binding]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class AdicionarPagamentoCartaoPostSteps
    {
        private readonly ScenarioContext _context;
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public AdicionarPagamentoCartaoPostSteps(ScenarioContext context, IntegrationTestsFixture<Startup> testsFixture)
        {
            _context = context;
            _testsFixture = testsFixture;
        }

        [Given(@"Que a solicitação de pagamento é enviada")]
        public void DadoQueASolicitacaoDePagamentoEEnviada()
        {
            // Given
            var faker = AutoFaker.Create();

            var adicionarPagamentoCartaoViewModelFaker = new AutoFaker<AdicionarPagamentoCartaoViewModel>()
                .RuleFor(p => p.AnoVencimentoCartao, f => (DateTime.Now.Year + 2))
                .RuleFor(p => p.MesVencimentoCartao, f => f.Random.Int(1, 12))
                .RuleFor(p => p.NumeroCartao, f => f.Finance.CreditCardNumber())
                .RuleFor(p => p.Valor, f => (double)f.Finance.Amount());

            var adicionarPagamentoCartaoViewModel = adicionarPagamentoCartaoViewModelFaker.Generate();

            _context.Set(adicionarPagamentoCartaoViewModel);
        }

        [When(@"A solicitação é processada")]
        public async Task QuandoASolicitacaoEProcessada()
        {
            var adicionarPagamentoCartaoViewModel = _context.Get<AdicionarPagamentoCartaoViewModel>();

            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/pagamento", adicionarPagamentoCartaoViewModel);

            _context.Set(postResponse);
        }

        [Then(@"O pagamento deverá ser adicionado")]
        public void EntaoOPagamentoDeveraSerAdicionado()
        {
            // Assert
            Assert.True(_context.Get<HttpResponseMessage>().IsSuccessStatusCode);
        }
    }
}
