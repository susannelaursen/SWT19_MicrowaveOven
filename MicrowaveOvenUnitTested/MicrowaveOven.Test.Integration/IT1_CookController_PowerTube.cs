using System;
using System.Collections.Generic;
using System.Linq;
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
    public class IT1_CookController_PowerTube // Susanne
    {
        private IDisplay _iDisplay;
        private ITimer _iTimer;
        private IUserInterface _ui;
        private ICookController _iCookController;
        private IPowerTube _uut;
        private IOutput _iOutput;

        [SetUp] 
        public void SetUp()
        {
            _uut = new PowerTube(_iOutput);
            _iDisplay = Substitute.For<IDisplay>();
            _iOutput = Substitute.For<IOutput>();
            _ui = Substitute.For<IUserInterface>();
            _iTimer = Substitute.For<ITimer>();
            _iCookController = new CookController(_iTimer, _iDisplay, _uut, _ui);
        }

        [TestCase(15, 50)]
        [TestCase(15, 15)]
        public void CookController_StartWasCalled_PowerTubeWasTurnedOn(int power, int time)
        {
            _iCookController.StartCooking(power, time);
            Thread.Sleep(1100);
            Assert.That(_iOutput.OutputLine(LogLine.CompareTo(50)));
        }
    }
}
