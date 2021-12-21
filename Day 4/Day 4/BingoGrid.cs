using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
    public class BingoGrid
    {
        public BingoCell[,] Cells { get; set; }

        public BingoGrid(BingoCell[,] cells)
        {
            Cells = cells;
        }

        public bool IsCompleted { get; set; } = false;

        public void MarkNumber(int value)
        {
            foreach (var cell in Cells)
            {
                if (cell.Value == value)
                {
                    cell.Mark();
                    return;
                }
            }
        }

        public bool IsBingo()
        {
            if (IsBingoInRow() || IsBingoInColumn()) {
                return true;
            }
            return false;
        }

        private bool IsBingoInRow()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                var bingoFound = true;
                for (int j = 0; j < Cells.GetLength(0); j++)
                {
                    if (!Cells[i, j].IsMarked)
                    {
                        bingoFound = false;
                        break;
                    }
                }
                if (bingoFound)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsBingoInColumn()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                var bingoFound = true;
                for (int j = 0; j < Cells.GetLength(0); j++)
                {
                    if (!Cells[j, i].IsMarked)
                    {
                        bingoFound = false;
                        break;
                    }
                }
                if (bingoFound)
                {
                    return true;
                }
            }
            return false;
        }

        internal int GetSumOfUnmarkedCells()
        {
            var sumOfUnmarkedCells = 0;
            foreach (var cell in Cells)
            {
                if (!cell.IsMarked)
                {
                    sumOfUnmarkedCells += cell.Value;
                }
            }
            return sumOfUnmarkedCells;
        }
    }
}
