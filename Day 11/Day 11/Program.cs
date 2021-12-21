using System;

namespace Day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day11.txt");
            puzzleManager.SolvePartOne(100);
            puzzleManager.SolvePartTwo();
        }
    }
}
