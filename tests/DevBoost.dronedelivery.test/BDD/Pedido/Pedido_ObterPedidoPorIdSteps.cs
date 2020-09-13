using AutoBogus;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Test.Model;
using Moq.AutoMock;
using TechTalk.SpecFlow;

namespace DevBoost.DroneDelivery.Test.BDD.Pedido
{
    [Binding]
    public class Pedido_ObterPedidoPorIdSteps : Cenario
    {
        public Pedido_ObterPedidoPorIdSteps(ScenarioContext context) : base(context) { }

        [Given(@"Que eu possua um pedido cadastrado")]
        public void DadoQueEuPossuaUmPedidoCadastrado()
        {
           
        }
        
        [When(@"Eu solicitar um pedido por Id")]
        public void QuandoEuSolicitarUmPedidoPorId()
        {
            
        }
        
        [Then(@"O pedido será retornado")]
        public void EntaoOPedidoSeraRetornado()
        {
           
        }
    }
}
