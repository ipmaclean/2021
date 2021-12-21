using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3
{
    internal static class InputHelper
    {
        internal static List<string> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var report = new List<string>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    report.Add(ln);
                }
            }
            return report;
        }
    }
}
