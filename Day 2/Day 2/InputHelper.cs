using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_2
{
    internal static class InputHelper
    {
        internal static List<Instruction> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var instructions = new List<Instruction>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                var commandRegex = new Regex(@"^[a-z]+");
                var numberRegex = new Regex(@"\d+$");

                while ((ln = sr.ReadLine()) != null)
                {
                    var command = commandRegex.Match(ln).Value;
                    var value = int.Parse(numberRegex.Match(ln).Value);

                    var instruction = new Instruction(command, value);
                    instructions.Add(instruction);
                }
            }
            return instructions;
        }
    }
}
