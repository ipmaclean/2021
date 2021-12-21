using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_21
{
    public class InputHelper
    {
        public static (int, int) Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var output = new List<string>();
            MatchCollection playerOneMatches;
            MatchCollection playerTwoMatches;
            using (var sr = new StreamReader(input))
            {
                var numberRegex = new Regex(@"\d+");
                playerOneMatches = numberRegex.Matches(sr.ReadLine());
                playerTwoMatches = numberRegex.Matches(sr.ReadLine());


            }
            return (int.Parse(playerOneMatches[1].Value), int.Parse(playerTwoMatches[1].Value));
        }
    }
}
