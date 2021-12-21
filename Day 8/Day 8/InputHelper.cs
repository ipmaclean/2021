using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_8
{
    public class InputHelper
    {
        public static List<Display> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var displays = new List<Display>();


            using (var sr = new StreamReader(input))
            {
                string ln;
                var wordRegex = new Regex(@"[a-z]+");
                while ((ln = sr.ReadLine()) != null)
                {
                    var matches = wordRegex.Matches(ln).Select(m => m.Value).ToArray();
                    var digits = matches.Take(10).ToArray();
                    var output = matches.TakeLast(4).ToArray();

                    var display = new Display(digits, output);
                    displays.Add(display);
                }
            }
            return displays;
        }
    }
}
