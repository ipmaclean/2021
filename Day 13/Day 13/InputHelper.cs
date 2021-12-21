using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_13
{
    public static class InputHelper
    {
        public static (List<(int, int)>, List<(string, int)>) Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var paperCoords = new List<(int, int)>();
            var instructions = new List<(string, int)>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var numberRegex = new Regex(@"\d+");

                while ((ln = sr.ReadLine()) != string.Empty)
                {
                    var numberMatches = numberRegex.Matches(ln);
                    paperCoords.Add((int.Parse(numberMatches[0].Value), int.Parse(numberMatches[1].Value)));
                }

                var coordRegex = new Regex(@"[xy]");
                while ((ln = sr.ReadLine()) != null)
                {
                    var coord = coordRegex.Match(ln).Value;
                    var number = numberRegex.Match(ln).Value;
                    instructions.Add((coord, int.Parse(number)));
                }
            }
            return (paperCoords, instructions);
        }
    }
}
