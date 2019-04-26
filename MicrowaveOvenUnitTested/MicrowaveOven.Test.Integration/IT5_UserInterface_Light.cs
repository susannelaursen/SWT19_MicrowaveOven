using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses;
using NSubstitute.Core;
using NUnit;
using System.Threading;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT5_UserInterface_Light //Susanne 
    {
        private IDisplay _iDisplay;
        private ITimer _iTimer;
        private IUserInterface _ui;
        private CookController _iCookController;
        private IPowerTube _iPowerTube;
        private IOutput _iOutput;
        private ILight _uut;

        private IButton _power;
        private IButton _time;
        private IButton _start;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {

            _iOutput = Substitute.For<IOutput>();
            _uut = new Light(_iOutput);
            _iDisplay = Substitute.For<IDisplay>();
            _power = Substitute.For<IButton>();
            _time = Substitute.For<IButton>();
            _start = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _iTimer = Substitute.For<ITimer>();
            _iCookController = new CookController(_iTimer, _iDisplay, _iPowerTube);

            _ui = new UserInterface(_power, _time, _start, _door, _iDisplay, _uut, _iCookController);
            _iCookController.UI = _ui;
            
        }

        [Test]
        public void StateReady_OnDoorOpened_TurnOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _iOutput.Received().OutputLine("Light is turned on");
        }
        [Test]
        public void StateDoorIsOpen_OnDoorOpend_TurnOn_ThenClosed_LightTurnOff()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _iOutput.Received().OutputLine("Light is turned off");
        }
        [TestCase(15,1)]
        public void StateSetTime_PressStartCancelButton_TurnOn(int power, int time)
        {
            //state ready
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            //Steate opend closed --> ready
            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //set power 
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //State set time
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _iOutput.Received().OutputLine("Display shows: 00:01");
        }
    }
}

 

