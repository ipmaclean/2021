using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9
{
    public class InputHelper
    {
        public static List<List<int>> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var heightMap = new List<List<int>>();


            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    var heights = new List<int>();
                    
                    foreach (var digit in ln)
                    {
                        heights.Add(digit - '0');
                    }
                    heightMap.Add(heights);
                }
            }
            return heightMap;
        }
    }
}
