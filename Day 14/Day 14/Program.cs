using System;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day14.txt");
            puzzleManager.Solve(10, "one");
            puzzleManager.Solve(40, "two");
        }
    }
}
