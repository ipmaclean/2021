using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_12
{
    public static class InputHelper
    {
        public static Cave[] Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var caves = new List<Cave>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var caveRegex = new Regex(@"[a-zA-z]+");
                while ((ln = sr.ReadLine()) != null)
                {
                    var caveMatches = caveRegex.Matches(ln);

                    for (int i = 0; i < 2; i++)
                    {
                        if (!caves.Any(c => c.Name == caveMatches[i].Value))
                        {
                            caves.Add(new Cave(caveMatches[i].Value));
                        }
                    }

                    var firstCave = caves.First(c => c.Name == caveMatches[0].Value);
                    var secondCave = caves.First(c => c.Name == caveMatches[1].Value);

                    firstCave.AdjacentCaves.Add(secondCave);
                    secondCave.AdjacentCaves.Add(firstCave);
                }
            }
            return caves.ToArray();
        }
    }
}
