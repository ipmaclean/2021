using System.Collections.Generic;
using System.IO;

namespace Day_10
{
    public class InputHelper
    {
        public static List<Queue<char>> Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var subsystem = new List<Queue<char>>();


            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    var line = new Queue<char>();

                    foreach (var character in ln)
                    {
                        line.Enqueue(character);
                    }
                    subsystem.Add(line);
                }
            }
            return subsystem;
        }
    }
}
