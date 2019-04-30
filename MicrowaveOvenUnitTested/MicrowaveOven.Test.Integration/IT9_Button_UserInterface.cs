using System;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT9_Button_UserInterface //Ines
    {
        private IUserInterface _ui;
        //private ICookController _cookController;

        private ITimer _timer;
        private IPowerTube _powertube;

        private IButton _uut_power;
        private IButton _uut_time;
        private IButton _uut_start;
        private IDoor _door;
        private ILight _light;
        private IOutput _output;
        private ICookController _cooker;
        private IDisplay _display;



        [SetUp]
        public void SetUp()
        {

            _timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();

            _uut_power = new Button();
            _uut_time = new Button();
            _uut_start = new Button();
            _door = Substitute.For<IDoor>();
            
            _output = Substitute.For<IOutput>();
            _cooker = Substitute.For<ICookController>();

            _display = new Display(_output);
            _light = new Light(_output);
            _powertube = new PowerTube(_output);
            _ui = new UserInterface(_uut_power, _uut_time, _uut_start, _door, _display, _light, _cooker);

        }

        [Test]
        public void UserInterface_Button_WasPowerPressed()
        {
            _uut_power.Press();
            _output.Received().OutputLine("Display shows: 50 W");

        }
        [Test]
        public void UserInterface_Button_WasPowerPressed_15times()
        {
            for (int i = 0; i < 15; i++)
            {
                _uut_power.Press();
             
            }
            _output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void UserInterface_Button_WasTimePressed()
        {
            _uut_power.Press();
            _uut_time.Press();
            _output.Received().OutputLine("Display shows: 01:00");

        }
        [Test]
        public void UserInterface_Button_WasStartCancelPressed()
        {
            _uut_power.Press();
            _uut_time.Press();
            _uut_start.Press();
            _output.Received().OutputLine("Light is turned on");
            _output.Received().OutputLine("PowerTube works with " + 7.14 + " %");
            Thread.Sleep(1100);
            _output.Received().OutputLine("Display shows: 00:59");
            _output.Received().OutputLine("Display shows: 50 W");
            _ui.CookingIsDone();
            _output.Received().OutputLine("Display cleared");
            _output.Received().OutputLine("Light is turned off");


        }

    }
}
