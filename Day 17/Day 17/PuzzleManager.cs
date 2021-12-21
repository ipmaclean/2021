using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_17
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<(int, int)> TargetCoords { get; set; }

        public int MaxHorizontalTargetCoord { get; set; }
        public int MinVerticalTargetCoord { get; set; }

        public int MinHorizontalVelocity { get; set; }
        public int MaxHorizontalVelocity { get; set; }
        public int MinVerticalVelocity { get; set; }
        public int MaxVerticalVelocity { get; set; }

        public PuzzleManager(string fileName)
        {
            InputFileName = fileName;
            TargetCoords = InputHelper.Parse(fileName);
            MaxHorizontalTargetCoord = TargetCoords.Max(x => x.Item1);
            MinVerticalTargetCoord = TargetCoords.Min(x => x.Item2);
            MinVerticalVelocity = MinVerticalTargetCoord;
            MaxVerticalVelocity = Math.Abs(MinVerticalTargetCoord) + 1;
            MaxHorizontalVelocity = MaxHorizontalTargetCoord;
            MinHorizontalVelocity = (int)(Math.Sqrt(2 * TargetCoords.Min(x => x.Item1) + 0.25) - 0.5);
        }

        public void Solve()
        {
            // Did some maths
            // Worked out closed form equations for the vertical and horizonal position after n steps
            // They're in the functions below

            var initialVelocitiesThatHitTarget = new List<(int, int)>();

            // hv = horizontalVelocity
            for (var hv = MinHorizontalVelocity; hv <= MaxHorizontalVelocity; hv++)
            {
                // vv = verticalVelocity
                for (var vv = MinVerticalVelocity; vv <= MaxVerticalVelocity; vv++)
                {
                    var positions = new List<(int, int)>();
                    var shouldContinueCalculatingSteps = true;
                    var step = 1;
                    while (shouldContinueCalculatingSteps)
                    {
                        int horizontalPosition = FindHorizontalPosition(hv, step);
                        int verticalPosition = FindVerticalPosition(vv, step);
                        positions.Add((horizontalPosition, verticalPosition));

                        if (horizontalPosition > MaxHorizontalTargetCoord || verticalPosition < MinVerticalTargetCoord)
                        {
                            shouldContinueCalculatingSteps = false;
                        }
                        step++;
                    }
                    var hitsTarget = TargetCoords.Select(x => x)
                                                 .Intersect(positions)
                                                 .Any();

                    if (hitsTarget)
                    {
                        initialVelocitiesThatHitTarget.Add((hv, vv));
                    }
                }
            }

            var largestVerticalVelocity = initialVelocitiesThatHitTarget.Max(x => x.Item2);

            var partOneSolution = largestVerticalVelocity * (largestVerticalVelocity + 1) / 2;
            Console.WriteLine($"The solution to part one is {partOneSolution}.");
            Console.WriteLine($"The solution to part two is {initialVelocitiesThatHitTarget.Count}.");
        }

        private int FindHorizontalPosition(int hv_0, int step)
        {
            if (step > hv_0)
            {
                return (hv_0 * (hv_0 + 1) / 2);
            }
            else
            {
                return hv_0 * step - (step * (step - 1) / 2);
            }
        }

        private int FindVerticalPosition(int vv_0, int step)
        {
            return vv_0 * step - (step * (step - 1) / 2);
        }
    }
}
