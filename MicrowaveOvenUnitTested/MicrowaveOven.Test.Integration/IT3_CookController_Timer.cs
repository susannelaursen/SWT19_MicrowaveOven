using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute.Core;
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

        [SetUp]
        public void SetUp()
        {
            _uut = new Timer();

            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _ui = Substitute.For<IUserInterface>();
            _cookController = new CookController(_uut,_display,_powerTube,_ui);
        }

        [TestCase(10, 60)]
        [TestCase(10, 10)]
        public void CookController_StartWasCalled_CorrectTimeSaved(int pow,int time)
        {
            _cookController.StartCooking(pow, time);
            Thread.Sleep(1100); // Et sekund plus lidt mere
            _display.Received().ShowTime(0,time-1);

            // Der blev fundet fejl. Den 
        }
    }
}
