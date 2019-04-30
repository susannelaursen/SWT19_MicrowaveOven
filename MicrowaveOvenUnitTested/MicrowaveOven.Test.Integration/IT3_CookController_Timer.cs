using System.Threading;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT3_CookController_Timer // Matilde
    {
        private ICookController _cookController;
        private Timer _uut;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IUserInterface _ui;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _uut = new Timer();
    
            _powerTube = new PowerTube(_output);
            _ui = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_uut,_display,_powerTube,_ui);
        }

        [TestCase(10, 60)]
        [TestCase(10, 10)]
        public void CookController_StartWasCalled_CorrectTimeSaved(int pow,int time)
        {
            _cookController.StartCooking(pow, time);
            Thread.Sleep(1100); // Et sekund plus lidt mere
            _display.Received().ShowTime(0,time-1);

            // Der blev fundet fejl. Programmet er opstillet som værende sekunder man indtaster men der fratrækkes som var det millisekunder.
            // Fejl rettet i Timer OnTimerEvent linje 47-48.
        }

        [Test]
        public void CookContoller_StopWaCalled_TimerWasStopped() // Tjek med underviser. Jeg vil gerne teste om _uut.Stop bliver kaldt. 
        {
            _cookController.Stop();
            Assert.That(() => _uut.Stop(), Throws.Nothing);
        }

        [TestCase(10, 2)]
        public void CookController_TimerExpired_TimerWasStopped(int power, int time)
        {
            _cookController.StartCooking(power,time);
            Thread.Sleep(4000); // Et sekund plus lidt mere
            _output.Received().OutputLine("PowerTube turned off");
        }
    }
}
