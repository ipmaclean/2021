using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_23
{
    class GameState
    {
        public PositionState PositionState { get; set; }
        public Dictionary<char, (int, int)> FinalDestinationDictionary { get; set; }
        public int Score { get; set; }

        public GameState(int score)
        {
            Score = score;
        }

        public GameState(int score, Dictionary<char, (int, int)> finalDestinationDictionary) : this(score)
        {
            FinalDestinationDictionary = new Dictionary<char, (int, int)>(finalDestinationDictionary);
        }
    }
}
