using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_20
{
    public class InputHelper
    {
        public static List<List<char>> ParseImage(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var image = new List<List<char>>();

            using (var sr = new StreamReader(input))
            {
                var ln = sr.ReadLine();
                sr.ReadLine();

                while ((ln = sr.ReadLine()) != null)
                {
                    var imageRow = ln.Replace(".", "0").Replace("#", "1").ToCharArray().ToList();
                    image.Add(imageRow);
                }
            }
            return image;
        }
        public static char[] ParseEnhancementAlgorithm(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var enhancementAlgorithm = new char[0];

            using (var sr = new StreamReader(input))
            {
                var ln = sr.ReadLine();
                enhancementAlgorithm = ln.Replace(".", "0").Replace("#", "1").ToCharArray();
            }
            return enhancementAlgorithm;
        }
    }
}
