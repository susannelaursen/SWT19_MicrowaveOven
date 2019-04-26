using System;
using NUnit.Framework;
using System.Threading;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT4_UserInterface_CookController // Matilde
    {
        private CookController _uut;
        private IUserInterface _ui;
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

            _uut = new CookController(_timer, _display, _powertube);
            _ui = new UserInterface(_power, _time, _start, _door, _display, _light, _uut);
        }

        [Test]
        public void CookController_StartCookingWasCalled()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this,EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            Thread.Sleep(1100); // Et sekund plus lidt mere

            _timer.Received().Start(Arg.Any<int>());
            
        }

        [Test]
        public void CookController_DoorWasOpened_StopWasCalled()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            _timer.Received().Stop();
        }

        [Test]
        public void CookController_CancelWasPushed_StopWasCalled()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _timer.Received().Stop();
        }
    }
}
