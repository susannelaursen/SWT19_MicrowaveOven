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
    class IT7_UserInterface_Display //Ines
    {
        private IUserInterface _ui;
        

        private ITimer _timer;
        private IPowerTube _powertube;

        private IButton _power;
        private IButton _time;
        private IButton _start;
        private IDoor _door;
        private ILight _light;
        private IOutput _output;
        private ICookController _cooker; 
        private Display _uut;
        


        [SetUp]
        public void SetUp()
        {

            _timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();

            _power = Substitute.For<IButton>();
            _time = Substitute.For<IButton>();
            _start = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _output = Substitute.For<IOutput>();
            _cooker = Substitute.For<ICookController>();

            _uut = new Display(_output);
            _ui = new UserInterface(_power, _time, _start, _door, _uut, _light, _cooker);

        }

        [Test]
        public void UserInterface_Display_WasPowerPressed()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display shows: 50 W");

        }

        [Test]
        public void UserInterface_Display_WasTimePressed()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
           
            //_start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display shows: 01:00");


        }

        [Test]
        public void UserInterface_Display_TimeWasPressedTwice()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display shows: 02:00");

        }
        [Test]
        public void UserInterface_Display_WasStartCancelPressed()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display shows: 01:00");

        }
        [Test]
        public void UserInterface_Display_WasCancelPressed()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine("Display shows: 01:00");

        }


    }
}
