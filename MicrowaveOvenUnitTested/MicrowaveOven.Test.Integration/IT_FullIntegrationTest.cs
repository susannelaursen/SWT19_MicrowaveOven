using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT_FullIntegrationTest
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startButton;
        private IDoor _door;

        private ITimer _timer;
        private IPowerTube _powertube;
        private ILight _light;
        private IOutput _output;
        private ICookController _cooker;
        private IDisplay _display;
        private IUserInterface _ui;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startButton = new Button();
            _door = new Door();

            _output = Substitute.For<IOutput>();
            _timer = new Timer();
            _powertube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _cooker = new CookController(_timer,_display,_powertube);
            _ui = new UserInterface(_powerButton,_timeButton,_startButton,_door,_display,_light,_cooker);
        }

        [Test]
        public void StartCooking_FullTest()
        {
            _door.Open();
            _door.Close();

            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();

            // Display shows: 150 W

            _timeButton.Press();
            _timeButton.Press();

            // Time shows 02:00

            _startButton.Press();

            _output.ReceivedCalls();
        }
    }
}
