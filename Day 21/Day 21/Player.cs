using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_21
{
    class Player
    {
        public int Number { get; set; }
        public int Position { get; set; }
        public int Score { get; set; } = 0;

        public Player(int number, int position)
        {
            Number = number;
            Position = position;
        }

        public void AdvancePosition(int steps)
        {
            Position -= 1;
            Position = (Position + steps) % 10;
            Position += 1;

            Score += Position;
        }
    }
}
