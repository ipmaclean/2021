using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_14
{
    public static class InputHelper
    {
        public static (string, Dictionary<string, string[]>) Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var initialPolymer = string.Empty;
            var polymerDictionary = new Dictionary<string, string[]>();

            using (var sr = new StreamReader(input))
            {
                initialPolymer = sr.ReadLine();
                var polymerRegex = new Regex(@"[A-Z]+");

                string ln = sr.ReadLine();
                while ((ln = sr.ReadLine()) != null)
                {
                    var polymerMatches = polymerRegex.Matches(ln);

                    var resultingPairs = new string[]
                    {
                        polymerMatches[0].Value[0] + polymerMatches[1].Value,
                        polymerMatches[1].Value + polymerMatches[0].Value[1]
                    };
                    polymerDictionary.Add(polymerMatches[0].Value, resultingPairs);
                }
            }
            return (initialPolymer, polymerDictionary);
        }
    }
}
