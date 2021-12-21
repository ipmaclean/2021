using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
    public class PuzzleManager
    {
        public List<int> Callouts { get; set; }

        public List<BingoGrid> BingoGrids { get; set; }

        public PuzzleManager(string fileName)
        {
            var (callouts, bingoEntries) = InputHelper.Parse(fileName);

            Callouts = callouts;
            BingoGrids = bingoEntries;
        }

        public void SolvePartOneAndTwo()
        {
            var bingoGridsRemaining = BingoGrids.Count;
            foreach (var callout in Callouts)
            {
                foreach (var bingoGrid in BingoGrids)
                {
                    if (!bingoGrid.IsCompleted)
                    {
                        bingoGrid.MarkNumber(callout);
                        if (bingoGrid.IsBingo())
                        {
                            if (bingoGridsRemaining == BingoGrids.Count)
                            {
                                Console.WriteLine($"The solution to part one is {callout * bingoGrid.GetSumOfUnmarkedCells()}.");
                            }
                            else if (bingoGridsRemaining == 1)
                            {
                                Console.WriteLine($"The solution to part two is {callout * bingoGrid.GetSumOfUnmarkedCells()}.");
                            }
                            bingoGrid.IsCompleted = true;
                            bingoGridsRemaining--;
                        }
                    }
                }
            }
        }
    }
}
