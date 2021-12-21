using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_13
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<(string, int)> Instructions { get; set; }
        public List<(int, int)> PaperCoords { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            (PaperCoords, Instructions) = InputHelper.Parse(inputFileName);
        }

        public void Solve()
        {
            var paperCoords = new List<(int, int)>(PaperCoords);
            var foldNumber = 0;
            foreach (var instruction in Instructions)
            {
                if (instruction.Item1 == "x")
                {
                    paperCoords = FoldAlongX(paperCoords, instruction.Item2);
                }
                else
                {
                    paperCoords = FoldAlongY(paperCoords, instruction.Item2);
                }
                if (foldNumber == 0)
                {
                    Console.WriteLine($"The solution to part one is {paperCoords.Count}.");
                }
                foldNumber++;
            }

            PrintPartTwoSolution(paperCoords);
        }

        private List<(int, int)> FoldAlongX(List<(int, int)> paperCoords, int foldLine)
        {
            var newCoords = new List<(int, int)>();
            foreach (var coord in paperCoords)
            {
                if (coord.Item1 == foldLine)
                {
                    continue;
                }
                else if (coord.Item1 < foldLine)
                {
                    newCoords.Add(coord);
                }
                else
                {
                    var newXCoord = foldLine - Math.Abs(foldLine - coord.Item1);

                    newCoords.Add((newXCoord, coord.Item2));
                }
            }
            return newCoords.Distinct().ToList();
        }

        private List<(int, int)> FoldAlongY(List<(int, int)> paperCoords, int foldLine)
        {
            var newCoords = new List<(int, int)>();
            foreach (var coord in paperCoords)
            {
                if (coord.Item2 == foldLine)
                {
                    continue;
                }
                else if (coord.Item2 < foldLine)
                {
                    newCoords.Add(coord);
                }
                else
                {
                    var newYCoord = foldLine - Math.Abs(foldLine - coord.Item2);

                    newCoords.Add((coord.Item1, newYCoord));
                }
            }
            return newCoords.Distinct().ToList();
        }

        private void PrintPartTwoSolution(List<(int, int)> paperCoords)
        {
            var minXCoord = paperCoords.Min(c => c.Item1);
            var maxXCoord = paperCoords.Max(c => c.Item1);
            var minYCoord = paperCoords.Min(c => c.Item2);
            var maxYCoord = paperCoords.Max(c => c.Item2);

            var width = maxXCoord - minXCoord + 1;
            var height = maxYCoord - minYCoord + 1;

            var paper = new char[width, height];

            for (int i = 0; i < width * height; i++) paper[i % width, i / width] = ' ';

            foreach (var coord in paperCoords)
            {
                paper[coord.Item1 - minXCoord, coord.Item2 - minYCoord] = '#';
            }

            Console.WriteLine("");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write(paper[i,j]);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
