using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    public class Octopus
    {
        public int EnergyLevel { get; set; }
        public bool HasFlashed { get; set; } = false;

        public Octopus(int energyLevel)
        {
            EnergyLevel = energyLevel;
        }

        public void PowerUp()
        {
            EnergyLevel++;
        }

        public void ResetPower()
        {
            HasFlashed = false;
            if (EnergyLevel > 9)
            {
                EnergyLevel = 0;
            }
        }
    }
}
