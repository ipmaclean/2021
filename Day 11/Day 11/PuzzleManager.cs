using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Octopus[,] Octopi { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        public void SolvePartOne(int numberOfSteps)
        {
            Octopi = InputHelper.Parse(InputFileName);
            long numberOfFlashes = 0;

            for (int step = 0; step < numberOfSteps; step++)
            {
                foreach (var octopus in Octopi)
                {
                    octopus.PowerUp();
                }

                while (Octopi.Cast<Octopus>().Any(o => o.EnergyLevel > 9 && !o.HasFlashed))
                {
                    for (int i = 0; i < Octopi.GetLength(0); i++)
                    {
                        for (int j = 0; j < Octopi.GetLength(0); j++)
                        {
                            if (Octopi[i,j].EnergyLevel > 9 && !Octopi[i, j].HasFlashed)
                            {
                                Octopi[i, j].HasFlashed = true;
                                numberOfFlashes++;
                                PowerUpAdjacentOctopi(i, j);
                            }
                        }
                    }
                }

                foreach (var octopus in Octopi)
                {
                    octopus.ResetPower();
                }
            }
            
            Console.WriteLine($"The solution to part one is {numberOfFlashes}.");
        }

        public void SolvePartTwo()
        {
            Octopi = InputHelper.Parse(InputFileName);
            var stepNumber = 0;

            while (!Octopi.Cast<Octopus>().All(o => o.EnergyLevel == 0))
            {
                foreach (var octopus in Octopi)
                {
                    octopus.PowerUp();
                }

                while (Octopi.Cast<Octopus>().Any(o => o.EnergyLevel > 9 && !o.HasFlashed))
                {
                    for (int i = 0; i < Octopi.GetLength(0); i++)
                    {
                        for (int j = 0; j < Octopi.GetLength(0); j++)
                        {
                            if (Octopi[i, j].EnergyLevel > 9 && !Octopi[i, j].HasFlashed)
                            {
                                Octopi[i, j].HasFlashed = true;
                                PowerUpAdjacentOctopi(i, j);
                            }
                        }
                    }
                }

                foreach (var octopus in Octopi)
                {
                    octopus.ResetPower();
                }
                stepNumber++;
            }

            Console.WriteLine($"The solution to part two is {stepNumber}.");
        }

        private void PowerUpAdjacentOctopi(int i, int j)
        {
            var adjacentCellHelper = new (int, int)[]
            {
                (-1, -1),
                (-1, 0),
                (-1, 1),
                (0, -1),
                (0, 1),
                (1, -1),
                (1, 0),
                (1, 1)
            };

            foreach (var cell in adjacentCellHelper)
            {
                if (i + cell.Item1 < 0 ||
                    i + cell.Item1 >= Octopi.GetLength(0) ||
                    j + cell.Item2 < 0 ||
                    j + cell.Item2 >= Octopi.GetLength(0))
                {
                    continue;
                }

                Octopi[i + cell.Item1, j + cell.Item2].PowerUp();
            }
        }
    }
}
