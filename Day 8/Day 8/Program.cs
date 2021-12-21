using System;

namespace Day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day8.txt");
            puzzleManager.SolvePartOne();
            puzzleManager.SolvePartTwo();
        }
    }
}
