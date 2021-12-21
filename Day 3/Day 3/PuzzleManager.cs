using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3
{
    class PuzzleManager
    {
        public List<string> Report { get; set; }

        public PuzzleManager(string fileName)
        {
            Report = InputHelper.Parse(fileName);
        }

        internal void SolvePartOne()
        {
            var gammaRate = string.Empty;
            var epsilonRate = string.Empty;

            for (int i = 0; i < Report[0].Length; i++)
            {
                var oneCount = 0;
                for (int j = 0; j < Report.Count; j++)
                {
                    if (Report[j][i] == '1')
                    {
                        oneCount++;
                    }
                }
                if (oneCount > Report.Count / 2)
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }
            Console.WriteLine($"The solution to part one is {Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)}.");
        }

        internal void SolvePartTwo()
        {
            var oxyGenRating = new List<string>(Report);
            var index = 0;
            while (oxyGenRating.Count > 1)
            {
                var popularDigit = 'a';
                // Calculate most/least common values at index

                var oneCount = 0;
                for (int j = 0; j < oxyGenRating.Count; j++)
                {
                    if (oxyGenRating[j][index] == '1')
                    {
                        oneCount++;
                    }
                }
                if (oneCount >= (int)Math.Ceiling((double)oxyGenRating.Count / 2))
                {
                    popularDigit = '1';
                }
                else
                {
                    popularDigit = '0';
                }


                // Remove the dud ratings
                for (int i = oxyGenRating.Count - 1; i >= 0; i--)
                {
                    if (oxyGenRating[i][index] != popularDigit)
                    {
                        oxyGenRating.RemoveAt(i);
                    }
                }
                index++;
            }


            var co2ScrubRating = new List<string>(Report);
            index = 0;
            while (co2ScrubRating.Count > 1)
            {
                var unpopularDigit = 'a';
                // Calculate most/least common values at index

                var zeroCount = 0;
                for (int j = 0; j < co2ScrubRating.Count; j++)
                {
                    if (co2ScrubRating[j][index] == '0')
                    {
                        zeroCount++;
                    }
                }
                if (zeroCount <= (int)Math.Floor((double)co2ScrubRating.Count / 2))
                {
                    unpopularDigit = '0';
                }
                else
                {
                    unpopularDigit = '1';
                }


                // Remove the dud ratings
                for (int i = co2ScrubRating.Count - 1; i >= 0; i--)
                {
                    if (co2ScrubRating[i][index] != unpopularDigit)
                    {
                        co2ScrubRating.RemoveAt(i);
                    }
                }
                index++;
            }

            Console.WriteLine($"The solution to part one is {Convert.ToInt32(oxyGenRating[0], 2) * Convert.ToInt32(co2ScrubRating[0], 2)}.");
        }

    }
}
