using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<List<int>> HeightMap { get; set; }
        public List<(int, int)> LowPoints { get; set; } = new List<(int, int)>();
        public List<int> BasinSizes { get; set; } = new List<int>();

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            HeightMap = InputHelper.Parse(inputFileName);
        }

        public void SolvePartOne()
        {
            var solution = 0;
            for (var i = 0; i < HeightMap.Count; i++)
            {
                for (var j = 0; j < HeightMap[i].Count; j++)
                {
                    if (IsLowPoint(i, j))
                    {
                        solution += HeightMap[i][j] + 1;
                        LowPoints.Add((i, j));
                    }
                }
            }
            Console.WriteLine($"The solution to part one is {solution}.");
        }

        private bool IsLowPoint(int i, int j)
        {
            var currentHeight = HeightMap[i][j];
            var adjacdentCoords = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

            foreach (var coord in adjacdentCoords)
            {
                if (i + coord.Item1 >= HeightMap.Count ||
                    i + coord.Item1 < 0 ||
                    j + coord.Item2 >= HeightMap[i].Count ||
                    j + coord.Item2 < 0)
                {
                    continue;
                }
                if(currentHeight >= HeightMap[i + coord.Item1][j + coord.Item2]) return false;
            }
            return true;
        }

        public void SolvePartTwo()
        {
            foreach (var lowPoint in LowPoints)
            {
                BasinSizes.Add(BasinHunter(lowPoint));
            }

            var maxBasinSizes = BasinSizes.OrderByDescending(x => x).Take(3);
            var solution = 1;

            foreach (var size in maxBasinSizes) solution *= size;

            Console.WriteLine($"The solution to part one is {solution}.");
        }

        private int BasinHunter((int, int) lowPoint)
        {
            var sizeOfBasin = 0;
            var pointsToCheck = new Queue<(int, int)>();
            var checkedPoints = new List<(int,int)>();
            pointsToCheck.Enqueue(lowPoint);

            while (pointsToCheck.Count > 0)
            {
                sizeOfBasin += FindAdjacentHigherPoints(pointsToCheck, checkedPoints);
            }
            return sizeOfBasin;
        }

        private int FindAdjacentHigherPoints(Queue<(int, int)> pointsToCheck, List<(int, int)> checkedPoints)
        {
            var pointToCheck = pointsToCheck.Dequeue();    
            
            if (checkedPoints.Contains(pointToCheck))
            {
                return 0;
            }

            var currentHeight = HeightMap[pointToCheck.Item1][pointToCheck.Item2];
            var adjacdentCoords = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

            foreach (var coord in adjacdentCoords)
            {
                if (pointToCheck.Item1 + coord.Item1 >= HeightMap.Count ||
                    pointToCheck.Item1 + coord.Item1 < 0 ||
                    pointToCheck.Item2 + coord.Item2 >= HeightMap[pointToCheck.Item1].Count ||
                    pointToCheck.Item2 + coord.Item2 < 0)
                {
                    continue;
                }
                if (currentHeight < HeightMap[pointToCheck.Item1 + coord.Item1][pointToCheck.Item2 + coord.Item2] &&
                    HeightMap[pointToCheck.Item1 + coord.Item1][pointToCheck.Item2 + coord.Item2] != 9 &&
                    !checkedPoints.Contains((pointToCheck.Item1 + coord.Item1, pointToCheck.Item2 +coord.Item2)))
                {
                    pointsToCheck.Enqueue((pointToCheck.Item1 + coord.Item1, pointToCheck.Item2 + coord.Item2));
                }
            }

            checkedPoints.Add(pointToCheck);
            return 1;
        }
    }
}
