using System;

namespace Day_22
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day22.txt");
            puzzleManager.Solve("one");
            puzzleManager.Solve("two");
        }
    }
}
