using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4
{
    public class BingoCell
    {
        public int Value { get; set; }

        public bool IsMarked { get; set; } = false;

        public BingoCell(int value)
        {
            Value = value;
        }

        public void Mark()
        {
            IsMarked = true;
        }
    }
}
