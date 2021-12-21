using System;

namespace Day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day6.txt");
            puzzleManager.Solve(80, "one");
            puzzleManager.Solve(256, "two");
        }
    }
}
