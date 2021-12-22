using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_22
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<Instruction> Instructions { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Instructions = InputHelper.Parse(inputFileName);
        }

        public void SolvePartOne()
        {
            var coordStatus = new Dictionary<(int, int, int), bool>();
            foreach (var instruction in Instructions)
            {
                if (Math.Abs(instruction.x1) > 50 || Math.Abs(instruction.x2) > 50) continue;

                for (int i = instruction.x1; i <= instruction.x2; i++)
                {
                    for (int j = instruction.y1; j <= instruction.y2; j++)
                    {
                        for (int k = instruction.z1; k <= instruction.z2; k++)
                        {
                            if (coordStatus.ContainsKey((i, j, k)))
                            {
                                if (instruction.Name == "on")
                                {
                                    coordStatus[(i, j, k)] = true;
                                }
                                else
                                {
                                    coordStatus[(i, j, k)] = false;
                                }
                            }
                            else
                            {
                                if (instruction.Name == "on")
                                {
                                    coordStatus.Add((i, j, k), true);
                                }
                                else
                                {
                                    coordStatus.Add((i, j, k), false);
                                }
                            }
                        }
                    }
                }
            }
            var solution = coordStatus.Values.Count(x => x);
            Console.WriteLine($"The solution to part one is {solution}.");
        }
    }
}
