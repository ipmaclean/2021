using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_18
{
    public class InputHelper
    {
        public static List<string> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var output = new List<string>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    output.Add(ln);
                }
            }
            return output;
        }
    }
}
