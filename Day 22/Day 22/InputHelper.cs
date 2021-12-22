using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_22
{
    public class InputHelper
    {
        public static List<Instruction> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var output = new List<Instruction>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                var numberRegex = new Regex(@"-*\d+");

                while ((ln = sr.ReadLine()) != null)
                {
                    var instructionName = string.Empty;
                    if (ln.Substring(0, 2) == "on")
                    {
                        instructionName = "on";
                    }
                    else if (ln.Substring(0, 3) == "off")
                    {
                        instructionName = "off";
                    }
                    else
                    {
                        throw new IOException("Bad input.");
                    }

                    var numberMatches = numberRegex.Matches(ln);

                    var instruction = new Instruction(instructionName,
                                                      int.Parse(numberMatches[0].Value),
                                                      int.Parse(numberMatches[1].Value),
                                                      int.Parse(numberMatches[2].Value),
                                                      int.Parse(numberMatches[3].Value),
                                                      int.Parse(numberMatches[4].Value),
                                                      int.Parse(numberMatches[5].Value));
                    output.Add(instruction);
                }
            }
            return output;
        }
    }
}
