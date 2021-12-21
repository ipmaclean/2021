using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2
{
    class Instruction
    {
        public string Command { get; set; }
        public int Value { get; set; }

        public Instruction(string command, int value)
        {
            Command = command;
            Value = value;
        }
    }
}
