using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1
{
    internal static class InputHelper
    {
        internal static List<int> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var depths = new List<int>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    depths.Add(int.Parse(ln));
                }
            }
            return depths;
        }
    }
}
