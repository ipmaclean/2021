using System;

namespace Day_22
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day22.txt");
            var partOneStart = DateTime.Now;
            puzzleManager.SolvePartOne();
            var partTwoStart = DateTime.Now;
            var partOneTime = partTwoStart - partOneStart;
            Console.WriteLine($"Time taken for part one - {partOneTime.TotalMilliseconds}ms.");
            //puzzleManager.SolvePartTwo();
            //var partTwoTime = DateTime.Now - partTwoStart;
            //Console.WriteLine($"Time taken for part two - {partTwoTime.TotalMilliseconds}ms.");
        }
    }
}
