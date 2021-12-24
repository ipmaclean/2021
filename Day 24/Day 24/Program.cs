using System;

namespace Day_24
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzleManager = new PuzzleManager("Test.txt");
            var puzzleManager = new PuzzleManager("Day24.txt");
            puzzleManager.Solve(59_996_912_981_939, "one");
            puzzleManager.Solve(17_241_911_811_915, "two");
        }
    }
}
