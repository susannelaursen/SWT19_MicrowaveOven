using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NUnit

namespace MicrowaveOven.Test.Integration
{
    [TestFixture]
    class IT2_CookController_Display
    {
        private IOutput _output;
        private ICookController _cookController;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IUserInterface _ui;
        private Display _uut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<Output>();
            _cookController = new CookController(_timer, _uut, _powerTube, _ui);
            _uut = new Display(_output);

        }
        [TestCase(0)]
        [TestCase()]
        public void 
    }
}
