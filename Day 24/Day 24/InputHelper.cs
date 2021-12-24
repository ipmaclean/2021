using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_24
{
    public class InputHelper
    {
        public static List<Instruction> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var instructions = new List<Instruction>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var instructionRegex = new Regex(@"\S+");
                while ((ln = sr.ReadLine()) != null)
                {
                    var instructionMatches = instructionRegex.Matches(ln);
                    if (instructionMatches.Count == 2)
                    {
                        var instruction = new Instruction(instructionMatches[0].Value, instructionMatches[1].Value);
                        instructions.Add(instruction);
                    }
                    else
                    {
                        var instruction = new Instruction(instructionMatches[0].Value, instructionMatches[1].Value, instructionMatches[2].Value);
                        instructions.Add(instruction);
                    }
                }
            }
            return instructions;
        }
    }
}
