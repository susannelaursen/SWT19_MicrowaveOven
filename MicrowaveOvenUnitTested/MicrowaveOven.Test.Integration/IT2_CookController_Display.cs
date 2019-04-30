using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT2_CookController_Display //Ines
    {
        private ICookController _cookController;
        private IOutput _output;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IUserInterface _ui;
        private Display _uut;


        [SetUp]
        public void SetUp()
        {  
            _output = Substitute.For<Output>();
            _timer = Substitute.For<ITimer>();
            //_powerTube = Substitute.For<IPowerTube>();
            _powerTube = new PowerTube(_output);
            _ui = Substitute.For<IUserInterface>();
            _cookController = new CookController(_timer, _uut, _powerTube, _ui);

            _uut = new Display(_output);

        }

        [TestCase(10, 60)]
        [TestCase(10, 10)]
        public void CookController_StartWasCalled_ShowsCorrectTime(int pow, int time)
        {
            _cookController.StartCooking(pow, time);
            Thread.Sleep(1100); 
            _output.Received().OutputLine("Display shows: 00:59");

          
        }

        //[TestCase(-2,1)]
        //[TestCase(150, 1)]
        //public void CookController_StartWasCalled_ShowsCorrectPower100w(int pow, int time)
        //{
        //    _cookController.StartCooking(pow, time);
        //    // Thread.Sleep(1100);
        //    _output.Received().OutputLine("Display shows: "+ pow+ " W");

        //}
        [Test]
        public void CookController_StartWasCalled_ShowsDisplayCleared()
        {
            _cookController.Stop();
            Thread.Sleep(1100);
            _output.Received().OutputLine("Display cleared");

        }
    }
}
