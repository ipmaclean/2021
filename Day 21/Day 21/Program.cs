using System;

namespace Day_21
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day21.txt");
            puzzleManager.SolvePartOne();
            //var partTwoStart = DateTime.Now;
            puzzleManager.SolvePartTwo();
            //var partTwoEnd = DateTime.Now - partTwoStart;
            //Console.WriteLine($"Time taken for part two - {partTwoEnd.TotalMilliseconds}ms.");
        }
    }
}
