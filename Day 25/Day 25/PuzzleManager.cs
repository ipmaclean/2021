using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_25
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            ColumnCount = InputHelper.CountColumns(InputFileName);
            RowCount = InputHelper.CountRows(InputFileName);
        }

        public void Solve()
        {
            // This really needs optimisation
            var cucumbers = InputHelper.Parse(InputFileName);

            var hasMovedThisTurn = true;
            var stepCount = 0;
            while (hasMovedThisTurn)
            {
                hasMovedThisTurn = false;
                FlagCucumbersThatWillMove(cucumbers, '>');
                foreach (var cucumber in cucumbers.Where(c => c.WillMove && c.Facing == '>'))
                {
                    cucumber.Move(ColumnCount, RowCount);
                    hasMovedThisTurn = true;
                }
                FlagCucumbersThatWillMove(cucumbers, 'v');
                foreach (var cucumber in cucumbers.Where(c => c.WillMove && c.Facing == 'v'))
                {
                    cucumber.Move(ColumnCount, RowCount);
                    hasMovedThisTurn = true;
                }
                stepCount++;
                //PrintPretty(cucumbers, RowCount, ColumnCount);
            }
            Console.WriteLine($"The solution to part one is {stepCount}.");
        }

        private void PrintPretty(List<Cucumber> cucumbers, int rowCount, int columnCount)
        {
            for (int j = 0; j < RowCount; j++) 
            {
                for (int i = 0; i < columnCount; i++)
                {
                    var cucumber = cucumbers.FirstOrDefault(c => c.Coords == (i, j));
                    if (cucumber != null)
                    {
                        Console.Write(cucumber.Facing);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.Write(Environment.NewLine);
            }
            Console.WriteLine("");
        }

        public void FlagCucumbersThatWillMove(List<Cucumber> cucumbers, char facing)
        {
            if (facing == '>')
            {
                foreach (var cucumber in cucumbers.Where(c => c.Facing == facing))
                {
                    if (cucumber.Coords.Item1 < ColumnCount - 1)
                    {
                        if (!cucumbers.Select(c => c.Coords).Contains((cucumber.Coords.Item1 + 1, cucumber.Coords.Item2)))
                        {
                            cucumber.WillMove = true;
                        }
                    }
                    else
                    {
                        if (!cucumbers.Select(c => c.Coords).Contains((0, cucumber.Coords.Item2)))
                        {
                            cucumber.WillMove = true;
                        }
                    }
                }
            }
            else
            {
                foreach (var cucumber in cucumbers.Where(c => c.Facing == facing))
                {
                    if (cucumber.Coords.Item2 < RowCount - 1)
                    {
                        if (!cucumbers.Select(c => c.Coords).Contains((cucumber.Coords.Item1, cucumber.Coords.Item2 + 1)))
                        {
                            cucumber.WillMove = true;
                        }
                    }
                    else
                    {
                        if (!cucumbers.Select(c => c.Coords).Contains((cucumber.Coords.Item1, 0)))
                        {
                            cucumber.WillMove = true;
                        }
                    }
                }
            }
            
        }
    }
}
