using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_19
{
    public class Beacon
    {
        public (int, int, int) RelativePosition { get; set; }
        public (int, int, int) AbsolutePosition { get; set; }
        public List<FingerPrint> FingerPrintsBetweenBeacons { get; set; } = new List<FingerPrint>();
        public int ScannerNumber { get; set; }

        public Beacon((int, int, int) relativePosition, int scannerNumber)
        {
            RelativePosition = relativePosition;
            ScannerNumber = scannerNumber;
        }

        public void AddFingerPrint(FingerPrint fingerprint)
        {
            FingerPrintsBetweenBeacons.Add(fingerprint);
        }
    }
}
