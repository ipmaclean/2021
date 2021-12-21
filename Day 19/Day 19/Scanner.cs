using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_19
{
    public class Scanner
    {
        public int Number { get; set; }
        public List<Beacon> Beacons { get; set; } = new List<Beacon>();
        public (int, int, int) PositionFromZero { get; set; }
        public bool Mapped { get; set; }

        public Scanner(int number)
        {
            Number = number;
        }

        public void SetBeaconDistances()
        {
            for (int i = 0; i < Beacons.Count - 1; i++)
            {
                for (int j = i + 1; j < Beacons.Count; j++)
                {
                    var distance = 0;
                    var minOffset = int.MaxValue;
                    var maxOffset = 0;

                    distance += Math.Abs(Beacons[i].RelativePosition.Item1 - Beacons[j].RelativePosition.Item1);
                    minOffset = Math.Min(minOffset, Math.Abs(Beacons[i].RelativePosition.Item1 - Beacons[j].RelativePosition.Item1));
                    maxOffset = Math.Max(maxOffset, Math.Abs(Beacons[i].RelativePosition.Item1 - Beacons[j].RelativePosition.Item1));

                    distance += Math.Abs(Beacons[i].RelativePosition.Item2 - Beacons[j].RelativePosition.Item2);
                    minOffset = Math.Min(minOffset, Math.Abs(Beacons[i].RelativePosition.Item2 - Beacons[j].RelativePosition.Item2));
                    maxOffset = Math.Max(maxOffset, Math.Abs(Beacons[i].RelativePosition.Item2 - Beacons[j].RelativePosition.Item2));

                    distance += Math.Abs(Beacons[i].RelativePosition.Item3 - Beacons[j].RelativePosition.Item3);
                    minOffset = Math.Min(minOffset, Math.Abs(Beacons[i].RelativePosition.Item3 - Beacons[j].RelativePosition.Item3));
                    maxOffset = Math.Max(maxOffset, Math.Abs(Beacons[i].RelativePosition.Item3 - Beacons[j].RelativePosition.Item3));

                    var fingerPrint = new FingerPrint(distance, minOffset, maxOffset);

                    Beacons[i].AddFingerPrint(fingerPrint);
                    Beacons[j].AddFingerPrint(fingerPrint);
                }
            }
        }
    }
}
