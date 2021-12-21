using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_16
{
    public static class InputHelper
    {
        public static Queue<char> Parse(string fileName)
        {
            var inputFile = Path.Combine("..", "..", "..", fileName);
            string input = string.Empty;

            using (var sr = new StreamReader(inputFile))
            {
                input = sr.ReadLine();
            }

            var binaryInput = String.Join(String.Empty, input.Select(
                                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            var output = new Queue<char>();
            foreach (var character in binaryInput)
            {
                output.Enqueue(character);
            }

            return output;
        }
    }
}
