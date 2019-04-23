using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit;

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT2_CookController_Display //Ines
    {
        private ICookController _cookController;
        private IOutput _output;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IUserInterface _ui;
        private Display _uut;


        [SetUp]
        public void SetUp()
        {  
            _output = Substitute.For<Output>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _ui = Substitute.For<IUserInterface>();
            _cookController = new CookController(_timer, _uut, _powerTube, _ui);

            _uut = new Display(_output);

        }

        [TestCase(10, 60)]
        [TestCase(10, 10)]
        public void CookController_StartWasCalled_ShowsCorrectTime(int pow, int time)
        {
            _cookController.StartCooking(pow, time);
            Thread.Sleep(1100); 
            _uut.Received().ShowTime(0, time - 1);


          
        }
        [TestCase(1, 100)]
        public void CookController_StartWasCalled_ShowsCorrectPower100w(int pow, int time)
        {
            _cookController.StartCooking(pow, time);
            Thread.Sleep(1100); 
            _uut.Received().ShowPower(100);

        }
        [Test]
        public void CookController_StartWasCalled_ShowsClearedDisplay()
        {
            _cookController.Stop();
            Thread.Sleep(1100);
            _uut.Received().Clear();

        }
    }
}
