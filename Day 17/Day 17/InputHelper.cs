using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_17
{
    public class InputHelper
    {
        public static List<(int, int)> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var targetCoords = new List<(int, int)>();
            using (var sr = new StreamReader(input))
            {
                var numberRegex = new Regex(@"-*\d+");
                string ln = sr.ReadLine();

                var numberMatches = numberRegex.Matches(ln);

                for (int x = int.Parse(numberMatches[0].Value); x <= int.Parse(numberMatches[1].Value); x++)
                {
                    for (int y = int.Parse(numberMatches[2].Value); y <= int.Parse(numberMatches[3].Value); y++)
                    {
                        targetCoords.Add((x, y));
                    }
                }
            }
            return targetCoords;
        }
    }
}
