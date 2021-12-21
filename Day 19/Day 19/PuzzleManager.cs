using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_19
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<List<Beacon>> UniqueBeacons { get; set; } = new List<List<Beacon>>();

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        internal void SolvePartOne()
        {
            var scanners = InputHelper.Parse(InputFileName);

            foreach (var scanner in scanners)
            {
                scanner.SetBeaconDistances();
            }

            for (int firstScannerIndex = 0; firstScannerIndex < scanners.Count; firstScannerIndex++)
            {
                for (int firstBeaconIndex = 0; firstBeaconIndex < scanners[firstScannerIndex].Beacons.Count; firstBeaconIndex++)
                {
                    var firstBeacon = scanners[firstScannerIndex].Beacons[firstBeaconIndex];
                    List<Beacon> uniqueBeaconList;
                    for (int secondScannerIndex = firstScannerIndex + 1; secondScannerIndex < scanners.Count; secondScannerIndex++)
                    {
                        // Search through other scanners for beacons with similar distance profiles
                        for (int secondBeaconIndex = 0; secondBeaconIndex < scanners[secondScannerIndex].Beacons.Count; secondBeaconIndex++)
                        {
                            var secondBeacon = scanners[secondScannerIndex].Beacons[secondBeaconIndex];

                            var matchCount = CountFingerPrintMatches(secondBeacon.FingerPrintsBetweenBeacons, firstBeacon.FingerPrintsBetweenBeacons);
                            if (matchCount >= 2)
                            {
                                // Look for the beacon.
                                if (UniqueBeacons.Any(ub => ub.Contains(firstBeacon) || ub.Contains(secondBeacon)))
                                {
                                    // If two unique beacons are in fact the same one, join the two lists.
                                    if (UniqueBeacons.Where(ub => ub.Contains(firstBeacon) || ub.Contains(secondBeacon)).Count() > 1)
                                    {
                                        var test = UniqueBeacons.Where(ub => ub.Contains(firstBeacon) || ub.Contains(secondBeacon)).ToList();
                                        test[0].AddRange(test[1]);
                                        test[0] = test[0].Distinct().ToList();
                                        UniqueBeacons.Remove(test[1]);
                                    }
                                    else
                                    {
                                        uniqueBeaconList = UniqueBeacons.First(ub => ub.Contains(firstBeacon) || ub.Contains(secondBeacon));

                                        if (!uniqueBeaconList.Contains(firstBeacon))
                                        {
                                            uniqueBeaconList.Add(firstBeacon);
                                        }
                                        if (!uniqueBeaconList.Contains(secondBeacon))
                                        {
                                            uniqueBeaconList.Add(secondBeacon);
                                        }
                                    }
                                }
                                // If the beacon isn't found, make a new uniqueBeaconList
                                else
                                {
                                    uniqueBeaconList = new List<Beacon>() { firstBeacon, secondBeacon };
                                    UniqueBeacons.Add(uniqueBeaconList);
                                }
                            }
                        }
                    }
                    if (!UniqueBeacons.Any(ub => ub.Contains(firstBeacon)))
                    {
                        uniqueBeaconList = new List<Beacon>() { firstBeacon };
                        UniqueBeacons.Add(uniqueBeaconList);
                    }
                }
            }

            Console.WriteLine($"The solution to part one is {UniqueBeacons.Count()}.");

            scanners[0].PositionFromZero = (0, 0, 0);
            scanners[0].Mapped = true;

            while (scanners.Any(s => !s.Mapped))
            {
                var mappedScanners = scanners.Where(s => s.Mapped).ToList();
                var unmappedScanners = scanners.Where(s => !s.Mapped).ToList();

                for (int i = 0; i < mappedScanners.Count; i++)
                {
                    for (int j = 0; j < unmappedScanners.Count; j++)
                    {
                        if (unmappedScanners[j].Mapped) continue;

                        var beaconsInBoth = new List<List<Beacon>>();

                        foreach (var unique in UniqueBeacons)
                        {
                            if (unique.Any(x => x.ScannerNumber == mappedScanners[i].Number) && unique.Any(x => x.ScannerNumber == unmappedScanners[j].Number))
                            {
                                beaconsInBoth.Add(unique);
                            }
                        }
                        if (beaconsInBoth.Count >= 2)
                        {
                            var takeTwo = beaconsInBoth.Take(2).ToList();


                            // Find the position vector between two beacons from the point of view of two scanners. The first scanner has the same alignment as the zero scanner.
                            var diffBetweenFirstScannerBeacons = (takeTwo[0].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item1 - takeTwo[1].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item1,
                                                                  takeTwo[0].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item2 - takeTwo[1].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item2,
                                                                  takeTwo[0].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item3 - takeTwo[1].First(x => x.ScannerNumber == mappedScanners[i].Number).RelativePosition.Item3);

                            var diffBetweenSecondScannerBeacons = (takeTwo[0].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item1 - takeTwo[1].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item1,
                                                                   takeTwo[0].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item2 - takeTwo[1].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item2,
                                                                   takeTwo[0].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item3 - takeTwo[1].First(x => x.ScannerNumber == unmappedScanners[j].Number).RelativePosition.Item3);

                            // Calculate the mapping needed to align the second scanner to the zero scanner.
                            var mapping = CalculateMapping(diffBetweenFirstScannerBeacons, diffBetweenSecondScannerBeacons);

                            // Check this is a valid mapping, if not, try again with a different pair of scanners.
                            if (mapping.Item1 == 0 || mapping.Item2 == 0 || mapping.Item3 == 0) throw new Exception();
                            if (Math.Abs(mapping.Item1) == Math.Abs(mapping.Item2) || Math.Abs(mapping.Item1) == Math.Abs(mapping.Item3) || Math.Abs(mapping.Item2) == Math.Abs(mapping.Item3)) continue;

                            var beaconFromFirstScanner = takeTwo[0].First(x => x.ScannerNumber == mappedScanners[i].Number);
                            var beaconFromSecondScanner = takeTwo[0].First(x => x.ScannerNumber == unmappedScanners[j].Number);

                            var mappedBeaconFromSecondScanner = ReturnMapping(mapping, beaconFromSecondScanner.RelativePosition);

                            // Find the position vector between the scanners using zero scanner alignment.
                            var secondScannerPositionFromFirstScanner = (beaconFromFirstScanner.RelativePosition.Item1 - mappedBeaconFromSecondScanner.Item1,
                                                                         beaconFromFirstScanner.RelativePosition.Item2 - mappedBeaconFromSecondScanner.Item2,
                                                                         beaconFromFirstScanner.RelativePosition.Item3 - mappedBeaconFromSecondScanner.Item3);

                            // Align the scanner's beacons to zero scanner's facing
                            foreach (var beacon in unmappedScanners[j].Beacons)
                            {
                                beacon.RelativePosition = ReturnMapping(mapping, beacon.RelativePosition);
                            }

                            // Find the scanner's position relative to zero scanner
                            unmappedScanners[j].PositionFromZero = (mappedScanners[i].PositionFromZero.Item1 + secondScannerPositionFromFirstScanner.Item1,
                                                                    mappedScanners[i].PositionFromZero.Item2 + secondScannerPositionFromFirstScanner.Item2,
                                                                    mappedScanners[i].PositionFromZero.Item3 + secondScannerPositionFromFirstScanner.Item3);

                            unmappedScanners[j].Mapped = true;
                        }
                    }
                }
            }

            var scannerDistances = new List<int>();
            for (int i = 0; i < scanners.Count - 1; i++)
            {
                for (int j = i + 1; j < scanners.Count; j++)
                {
                    scannerDistances.Add(Math.Abs(scanners[i].PositionFromZero.Item1 - scanners[j].PositionFromZero.Item1) +
                                         Math.Abs(scanners[i].PositionFromZero.Item2 - scanners[j].PositionFromZero.Item2) +
                                         Math.Abs(scanners[i].PositionFromZero.Item3 - scanners[j].PositionFromZero.Item3));
                }
            }
            Console.WriteLine($"The solution to part two is {scannerDistances.Max()}.");
        }

        private (int, int, int) ReturnMapping((int, int, int) mapping, (int, int, int) beaconMapping)
        {
            var mappedBeaconFromZero = (0, 0, 0);

            if (mapping.Item1 == 1)
            {
                mappedBeaconFromZero.Item1 = beaconMapping.Item1;
            }
            else if (mapping.Item1 == -1)
            {
                mappedBeaconFromZero.Item1 = -beaconMapping.Item1;
            }
            else if (mapping.Item1 == 2)
            {
                mappedBeaconFromZero.Item1 = beaconMapping.Item2;
            }
            else if (mapping.Item1 == -2)
            {
                mappedBeaconFromZero.Item1 = -beaconMapping.Item2;
            }
            else if (mapping.Item1 == 3)
            {
                mappedBeaconFromZero.Item1 = beaconMapping.Item3;
            }
            else if (mapping.Item1 == -3)
            {
                mappedBeaconFromZero.Item1 = -beaconMapping.Item3;
            }
            if (mapping.Item2 == 1)
            {
                mappedBeaconFromZero.Item2 = beaconMapping.Item1;
            }
            else if (mapping.Item2 == -1)
            {
                mappedBeaconFromZero.Item2 = -beaconMapping.Item1;
            }
            else if (mapping.Item2 == 2)
            {
                mappedBeaconFromZero.Item2 = beaconMapping.Item2;
            }
            else if (mapping.Item2 == -2)
            {
                mappedBeaconFromZero.Item2 = -beaconMapping.Item2;
            }
            else if (mapping.Item2 == 3)
            {
                mappedBeaconFromZero.Item2 = beaconMapping.Item3;
            }
            else if (mapping.Item2 == -3)
            {
                mappedBeaconFromZero.Item2 = -beaconMapping.Item3;
            }
            if (mapping.Item3 == 1)
            {
                mappedBeaconFromZero.Item3 = beaconMapping.Item1;
            }
            else if (mapping.Item3 == -1)
            {
                mappedBeaconFromZero.Item3 = -beaconMapping.Item1;
            }
            else if (mapping.Item3 == 2)
            {
                mappedBeaconFromZero.Item3 = beaconMapping.Item2;
            }
            else if (mapping.Item3 == -2)
            {
                mappedBeaconFromZero.Item3 = -beaconMapping.Item2;
            }
            else if (mapping.Item3 == 3)
            {
                mappedBeaconFromZero.Item3 = beaconMapping.Item3;
            }
            else if (mapping.Item3 == -3)
            {
                mappedBeaconFromZero.Item3 = -beaconMapping.Item3;
            }
            return mappedBeaconFromZero;
        }

        private (int, int, int) CalculateMapping((int, int, int) diffBetweenFirstBeacons, (int, int, int) diffBetweenSecondBeacons)
        {
            var mapping = (0, 0, 0);

            if (Math.Abs(diffBetweenFirstBeacons.Item1) == Math.Abs(diffBetweenSecondBeacons.Item1))
            {
                if (diffBetweenFirstBeacons.Item1 == diffBetweenSecondBeacons.Item1)
                    mapping.Item1 = 1;
                else
                    mapping.Item1 = -1;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item1) == Math.Abs(diffBetweenSecondBeacons.Item2))
            {
                if (diffBetweenFirstBeacons.Item1 == diffBetweenSecondBeacons.Item2)
                    mapping.Item1 = 2;
                else
                    mapping.Item1 = -2;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item1) == Math.Abs(diffBetweenSecondBeacons.Item3))
            {
                if (diffBetweenFirstBeacons.Item1 == diffBetweenSecondBeacons.Item3)
                    mapping.Item1 = 3;
                else
                    mapping.Item1 = -3;
            }
            if (Math.Abs(diffBetweenFirstBeacons.Item2) == Math.Abs(diffBetweenSecondBeacons.Item1))
            {
                if (diffBetweenFirstBeacons.Item2 == diffBetweenSecondBeacons.Item1)
                    mapping.Item2 = 1;
                else
                    mapping.Item2 = -1;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item2) == Math.Abs(diffBetweenSecondBeacons.Item2))
            {
                if (diffBetweenFirstBeacons.Item2 == diffBetweenSecondBeacons.Item2)
                    mapping.Item2 = 2;
                else
                    mapping.Item2 = -2;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item2) == Math.Abs(diffBetweenSecondBeacons.Item3))
            {
                if (diffBetweenFirstBeacons.Item2 == diffBetweenSecondBeacons.Item3)
                    mapping.Item2 = 3;
                else
                    mapping.Item2 = -3;
            }
            if (Math.Abs(diffBetweenFirstBeacons.Item3) == Math.Abs(diffBetweenSecondBeacons.Item1))
            {
                if (diffBetweenFirstBeacons.Item3 == diffBetweenSecondBeacons.Item1)
                    mapping.Item3 = 1;
                else
                    mapping.Item3 = -1;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item3) == Math.Abs(diffBetweenSecondBeacons.Item2))
            {
                if (diffBetweenFirstBeacons.Item3 == diffBetweenSecondBeacons.Item2)
                    mapping.Item3 = 2;
                else
                    mapping.Item3 = -2;
            }
            else if (Math.Abs(diffBetweenFirstBeacons.Item3) == Math.Abs(diffBetweenSecondBeacons.Item3))
            {
                if (diffBetweenFirstBeacons.Item3 == diffBetweenSecondBeacons.Item3)
                    mapping.Item3 = 3;
                else
                    mapping.Item3 = -3;
            }

            return mapping;
        }

        private int CountFingerPrintMatches(List<FingerPrint> fingerPrintsBetweenBeacons1, List<FingerPrint> fingerPrintsBetweenBeacons2)
        {
            var matchCount = 0;
            foreach (var fingerprint1 in fingerPrintsBetweenBeacons1)
            {
                foreach (var fingerprint2 in fingerPrintsBetweenBeacons2)
                {
                    if (fingerprint1.Equals(fingerprint2))
                    {
                        matchCount++;
                    }
                }
            }
            return matchCount;
        }
    }
}
