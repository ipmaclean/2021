using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    public static class InputHelper
    {
        public static Octopus[,] Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var octopi = new Octopus[10,10];


            using (var sr = new StreamReader(input))
            {
                string ln;
                var rowIndex = 0;
                while ((ln = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < ln.Length; i++)
                    {
                        octopi[rowIndex,i] = new Octopus(ln[i] - '0');
                    }
                    rowIndex++;
                }
            }
            return octopi;
        }
    }
}
