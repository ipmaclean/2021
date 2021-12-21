using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<Display> Displays { get; set; }
        public Dictionary<string, string> DisplayToNumeralDict { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Displays = InputHelper.Parse(inputFileName);
            DisplayToNumeralDict = new Dictionary<string, string>()
            {
                { "abcefg" , "0" },
                { "cf"     , "1" },
                { "acdeg"  , "2" },
                { "acdfg"  , "3" },
                { "bcdf"   , "4" },
                { "abdfg"  , "5" },
                { "abdefg" , "6" },
                { "acf"    , "7" },
                { "abcdefg", "8" },
                { "abcdfg" , "9" },
            };
        }

        public void SolvePartOne()
        {
            var solution = 0;

            foreach (var display in Displays)
            {
                solution += display.CountPartOneDigits();
            }

            Console.WriteLine($"The solution to part one is {solution}.");
        }

        public void SolvePartTwo()
        {
            var solution = 0;

            foreach (var display in Displays)
            {
                solution += display.CalculateOutput(DisplayToNumeralDict);
            }

            Console.WriteLine($"The solution to part two is {solution}.");
        }

        
    }
}
