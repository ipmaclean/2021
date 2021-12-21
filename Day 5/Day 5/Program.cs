using System;

namespace Day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day5.txt");
            puzzleManager.SolvePartOne();
            puzzleManager.SolvePartTwo();
        }
    }
}
