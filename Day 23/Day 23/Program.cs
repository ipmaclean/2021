using System;

namespace Day_23
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            //var puzzleManager = new PuzzleManager("TestPartOne.txt");
            var puzzleManager = new PuzzleManager("Day23PartOne.txt");
            puzzleManager.Solve("one");
            var endPartOneTime = DateTime.Now;
            Console.WriteLine($"Time for part one: {(endPartOneTime - startTime).TotalMilliseconds}ms.");

            //puzzleManager = new PuzzleManager("TestPartTwo.txt");
            puzzleManager = new PuzzleManager("Day23PartTwo.txt");
            puzzleManager.Solve("two");
            var endPartTwoTime = DateTime.Now;
            Console.WriteLine($"Time for part two: {(endPartTwoTime - endPartOneTime).TotalMilliseconds}ms.");
        }
    }
}
