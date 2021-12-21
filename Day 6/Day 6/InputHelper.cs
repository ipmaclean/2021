using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_6
{
    public class InputHelper
    {
        public static Dictionary<int, long> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var fish = new Dictionary<int, long>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 },
            };
            using (var sr = new StreamReader(input))
            {
                string ln;
                var numberRegex = new Regex(@"\d+");
                while ((ln = sr.ReadLine()) != null)
                {
                    var numberMatches = numberRegex.Matches(ln);
                    for (int i = 0; i < numberMatches.Count; i++)
                    {
                        fish[int.Parse(numberMatches[i].Value)]++;
                    }
                }
            }
            return fish;
        }
    }
}
