using System;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day2.txt");
            puzzleManager.SolvePartOne();
            puzzleManager.SolvePartTwo();
        }
    }
}
