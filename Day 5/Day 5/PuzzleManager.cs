using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5
{
    class PuzzleManager
    {
        public Dictionary<(int, int), int> VentsPartOne { get; set; }
        public Dictionary<(int, int), int> VentsPartTwo { get; set; }

        public PuzzleManager(string fileName)
        {
            VentsPartOne = InputHelper.ParsePartOne(fileName);
            VentsPartTwo = InputHelper.ParsePartTwo(fileName);
        }

        public void SolvePartOne()
        {
            var count = 0;

            foreach (var item in VentsPartOne.Values)
            {
                if (item > 1)
                {
                    count++;
                }
            }

            Console.WriteLine($"The solution to part one is {count}.");
        }

        public void SolvePartTwo()
        {
            var count = 0;

            foreach (var item in VentsPartTwo.Values)
            {
                if (item > 1)
                {
                    count++;
                }
            }

            Console.WriteLine($"The solution to part two is {count}.");
        }
    }
}
