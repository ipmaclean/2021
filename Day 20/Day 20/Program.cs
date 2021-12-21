using System;

namespace Day_20
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day20.txt");
            puzzleManager.Solve(2, "one");
            puzzleManager.Solve(50, "two");
        }
    }
}
