using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6
{
    public class PuzzleManager
    {
        public string InputFileName { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        public void Solve(int daysToModel, string solutionPart)
        {
            var fish = InputHelper.Parse(InputFileName);

            for (int i = 0; i < daysToModel; i++)
            {
                var zeroDayFishValue = fish[0];
                for (int j = 0; j < fish.Count - 1; j++)
                {
                    fish[j] = fish[j + 1];
                    if (j == 6)
                    {
                        fish[j] += zeroDayFishValue;
                    }
                }
                fish[8] = zeroDayFishValue;
            }
            var solution = fish.Values.Sum();
            Console.WriteLine($"The solution to part {solutionPart} is {solution}.");
        }
    }
}
