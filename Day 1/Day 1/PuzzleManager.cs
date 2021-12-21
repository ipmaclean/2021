using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1
{
    internal class PuzzleManager
    {
        public List<int> Depths { get; set; }
        public PuzzleManager(string fileName)
        {
            Depths = InputHelper.Parse(fileName);
        }

        internal void SolvePartOne()
        {
            var depthIncreases = 0;
            var previousDepth = Depths[0];

            for (var i = 1; i < Depths.Count; i++)
            {
                if (Depths[i] > previousDepth)
                {
                    depthIncreases++;
                }
                previousDepth = Depths[i];
            }
            Console.WriteLine($"The solution to part one is {depthIncreases}.");
        }

        internal void SolvePartTwo()
        {
            var depthIncreases = 0;
            var previousDepthSum = Depths[0] + Depths[1] + Depths[2];

            for (var i = 3; i < Depths.Count; i++)
            {
                var currentDepthSum = previousDepthSum - Depths[i - 3] + Depths[i];
                if (currentDepthSum > previousDepthSum)
                {
                    depthIncreases++;
                }
                previousDepthSum = currentDepthSum;
            }
            Console.WriteLine($"The solution to part two is {depthIncreases}.");
        }
    }
}
