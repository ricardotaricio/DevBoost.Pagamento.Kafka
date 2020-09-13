using AutoBogus;
using Moq.AutoMock;
using TechTalk.SpecFlow;

namespace DevBoost.DroneDelivery.Test.Model
{
    public class Cenario
    {

        public ScenarioContext _context;
        //public readonly IAutoFaker _autoFaker;
        //public readonly AutoMocker _mocker;
        //public Cenario(ScenarioContext context, IAutoFaker autoFaker, AutoMocker mocker)
        //{

        //    _context = context;
        //    _autoFaker = autoFaker;
        //    _mocker = mocker;
        //}
        public Cenario(ScenarioContext context)
        {

            _context = context;
        }



    }
}
