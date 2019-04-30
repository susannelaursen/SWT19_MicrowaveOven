﻿using System;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;
        private double power;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(double powerWatt)
        {
            power = ((powerWatt / 700) * 100); // Rettelse

            if (power < 1 || 100 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 100 % (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power} %");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}