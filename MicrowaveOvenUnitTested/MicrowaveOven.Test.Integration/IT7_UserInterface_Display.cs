using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private ICookController _cookController;
        private IOutput _output;
        private ITimer _timer;
        private IPowerTube _powerTube;
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

        [Test]
        public void UserInterface_DisplayTime()
        {

        }

    }
}
