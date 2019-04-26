using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MicrowaveOven;
using NSubstitute;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute.Core;
using NSubstitute.ReceivedExtensions;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT6_CookController_UserInterface // Matilde
    {
        private UserInterface _uut;
        private CookController _cookController;
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powertube;

        private IButton _power;
        private IButton _time;
        private IButton _start;
        private IDoor _door;
        private ILight _light;


        [SetUp]
        public void SetUp()
        {
            _timer = Substitute.For<ITimer>();
            _display = Substitute.For<IDisplay>();
            _powertube = Substitute.For<IPowerTube>();

            _power = Substitute.For<IButton>();
            _time = Substitute.For<IButton>();
            _start = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();

            _cookController = new CookController(_timer, _display, _powertube);
            _uut = new UserInterface(_power, _time, _start, _door, _display, _light, _cookController);
            _cookController.UI = _uut;

        }

        [Test]
        public void UserInterface_CookingIsDoneWasCalled()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timer.Expired += Raise.EventWith(this,EventArgs.Empty);

            _display.Received().Clear();
        }

    }
}
