using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_21
{
    class DeterministicDie
    {
        public int CurrentValue { get; set; } = 1;
        public int TimesRolled { get; set; } = 0;

        public int RollXTimes(int timesToRoll)
        {
            var output = 0;
            for (int i = 0; i < timesToRoll; i++)
            {
                output += CurrentValue;
                AdvanceCurrentValue();
                TimesRolled++;
            }
            return output;
        }

        private void AdvanceCurrentValue()
        {
            CurrentValue--;
            CurrentValue = (CurrentValue + 1) % 100;
            CurrentValue++;
        }
    }
}
