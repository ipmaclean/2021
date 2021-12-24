using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_24
{
    public class Instruction
    {
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string? Value2 { get; set; }

        public Instruction(string name, string value1)
        {
            Name = name;
            Value1 = value1;
        }

        public Instruction(string name, string value1, string value2) : this(name, value1)
        {
            Value2 = value2;
        }
    }
}
