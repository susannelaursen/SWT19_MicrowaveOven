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
    class IT8_Door_UserInterface //Susanne
    {
        private IDisplay _iDisplay;
        private ITimer _iTimer;
        private IUserInterface _uut;
        private CookController _iCookController;
        private IPowerTube _iPowerTube;
        private IOutput _iOutput;
        private ILight _iLight;

        private IButton _power;
        private IButton _time;
        private IButton _start;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _power = Substitute.For<IButton>();
            _time = Substitute.For<IButton>();
            _start = Substitute.For<IButton>();

            _iOutput = Substitute.For<IOutput>();
            _iLight = new Light(_iOutput);
            _iDisplay = Substitute.For<IDisplay>();
            _power = Substitute.For<IButton>();
            _time = Substitute.For<IButton>();
            _start = Substitute.For<IButton>();
            _door = new Door();
            _iTimer = Substitute.For<ITimer>();
            _iPowerTube = new PowerTube(_iOutput);
            _iCookController = new CookController(_iTimer, _iDisplay, _iPowerTube);

            _uut = new UserInterface(_power, _time, _start, _door, _iDisplay, _iLight, _iCookController);
            _iCookController.UI = _uut;

        }

        [Test]
        public void StateCooking_OpensDoor_OutputSaysLightTurnOn()
        {
            _door.Open();
            _iOutput.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void StateDoorIsOpen_ClosesDoor_OutputSaysLightTurnOff()
        {
            _door.Open();
            _door.Close();
            _iOutput.Received().OutputLine("Light is turned off");
        }
        [Test]
        public void StateCooking_OpensDoor_OutputSaysPowertubeTurnOff()
        {
            //Steate opend closed --> ready
            _power.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //set power 
            _time.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //State set time
            _start.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Open();
            _iOutput.Received().OutputLine("PowerTube turned off");
        }

    }
}
