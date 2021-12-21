using System;

namespace Day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day3.txt");
            puzzleManager.SolvePartOne();
            puzzleManager.SolvePartTwo();
        }
    }
}
