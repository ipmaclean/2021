using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_25
{
    public class InputHelper
    {
        public static List<Cucumber> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var cucumbers = new List<Cucumber>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var rowIndex = 0;
                while ((ln = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < ln.Length; i ++)
                    {
                        if (ln[i] == '.') continue;
                        var cucumber = new Cucumber(ln[i], (i, rowIndex));
                        cucumbers.Add(cucumber);
                    }
                    rowIndex++;
                }
            }
            return cucumbers;
        }

        public static int CountRows(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var file = new FileInfo(input);
            return File.ReadLines(file.FullName).Count();
        }

        public static int CountColumns(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            using (var sr = new StreamReader(input))
            {
                return sr.ReadLine().Length;
            }
        }
    }
}
