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



        [SetUp] 
        public void SetUp()
        {
            _iOutput = Substitute.For<IOutput>();
            _uut = new PowerTube(_iOutput);
            _iDisplay = Substitute.For<IDisplay>();
            
            _ui = Substitute.For<IUserInterface>();
            _iTimer = Substitute.For<ITimer>();
            _iCookController = new CookController(_iTimer, _iDisplay, _uut, _ui);
        }

        [TestCase(15, 50)]
        [TestCase(50, 15)]
        public void CookController_StartWasCalled_PowerTubeWasTurnedOn(int power, int time)
        {
            _iCookController.StartCooking(power, time);
            Thread.Sleep(1100);
            _iOutput.Received().OutputLine("PowerTube works with "+power+" %");
        }

        [TestCase(50, 1)]
        public void CookController_OnTimerExpired_PowerTubeWasTurnedOff(int power, int time)
        {
            _iCookController.StartCooking(power, time);
            _iTimer.Expired += Raise.EventWith(this, EventArgs.Empty);
            _iOutput.Received().OutputLine("PowerTube turned off");
        }

        //Tester metoden stop som bruges når døren åbnes eller der trykkes på stop-knappen
        [TestCase(15, 50)]
        [TestCase(50, 15)]
        public void CookController_StopWasCalled_PowerTubeWasTurnedOff(int power, int time)
        {
            _iCookController.StartCooking(power, time);
            _iCookController.Stop();
            _iOutput.Received().OutputLine("PowerTube turned off");
        }
    }
}
