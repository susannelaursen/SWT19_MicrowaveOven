using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework; 

namespace MicrowaveOven.Test.Integration
{
    public class IT1_CookController_PowerTube
    {
        private IDisplay iDisplay_;
        private ITimer iTimer_;
        private IUserInterface ui_;
        private ICookController iCookController_();
        private IPowerTube uut_;

        [Setup]
        public void SetUp()
        {
            iOutput_ = new Output();
            iCookController_ = new CookController();
            uut_ = new PowerTube();
        }

        public void TurnOn_True()
        {

        }
    }
}
