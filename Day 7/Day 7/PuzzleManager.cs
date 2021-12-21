using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7
{
    public class PuzzleManager
    {
        public string InputFileName { get; set; }
        public int[] Crabs { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Crabs = InputHelper.Parse(InputFileName);
        }

        public void SolvePartOne()
        {
            var smallest = Crabs.Min();
            var biggest = Crabs.Max();
            var distances = new Dictionary<int, long>();

            for (int i = smallest; i <= biggest; i++)
            {
                long distance = 0;
                foreach (var crab in Crabs)
                {
                    distance += Math.Abs(i - crab);
                }
                distances.Add(i, distance);
            }

            var leastFuel = distances.Values.Min();
            Console.WriteLine($"The solution to part one is {leastFuel}.");
        }

        public void SolvePartTwo()
        {
            var smallest = Crabs.Min();
            var biggest = Crabs.Max();
            var fuelCosts = new Dictionary<int, long>();

            for (int i = smallest; i <= biggest; i++)
            {
                long fuelCost = 0;
                foreach (var crab in Crabs)
                {
                    var difference = Math.Abs(i - crab);
                    fuelCost += (int)(difference * (difference + 1) * 0.5);
                }
                fuelCosts.Add(i, fuelCost);
            }

            var leastFuel = fuelCosts.Values.Min();
            Console.WriteLine($"The solution to part two is {leastFuel}.");
        }
    }
}
