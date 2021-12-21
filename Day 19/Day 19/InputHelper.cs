using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_19
{
    public class InputHelper
    {
        public static List<Scanner> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var output = new List<Scanner>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                var numberRegex = new Regex(@"-*\d+");

                while ((ln = sr.ReadLine()) != null)
                {
                    var numberMatch = numberRegex.Match(ln);
                    var scanner = new Scanner(int.Parse(numberMatch.Value));

                    while ((ln = sr.ReadLine()) != string.Empty && ln != null)
                    {
                        var numberMatches = numberRegex.Matches(ln);
                        var beacon = new Beacon((int.Parse(numberMatches[0].Value),
                                                 int.Parse(numberMatches[1].Value),
                                                 int.Parse(numberMatches[2].Value)),
                                                 scanner.Number);
                        scanner.Beacons.Add(beacon);
                    }
                    output.Add(scanner);
                }
            }
            return output;
        }
    }
}
