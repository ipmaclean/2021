using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_7
{
    public class InputHelper
    {
        public static int[] Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var crabs = new List<int>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                var numberRegex = new Regex(@"\d+");
                while ((ln = sr.ReadLine()) != null)
                {
                    var numberMatches = numberRegex.Matches(ln);
                    for (int i = 0; i < numberMatches.Count; i++)
                    {
                        crabs.Add(int.Parse(numberMatches[i].Value));
                    }
                }
            }
            return crabs.ToArray();
        }
    }
}
