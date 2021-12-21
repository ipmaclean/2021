using System;

namespace Day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day7.txt");
            puzzleManager.SolvePartOne();
            puzzleManager.SolvePartTwo();
        }
    }
}
