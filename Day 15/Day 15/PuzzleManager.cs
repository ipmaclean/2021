using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_15
{
    public class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Node[,] CavernPartOne { get; set; }
        public Node[,] CavernPartTwo { get; set; }
        public List<Node> UnvisitedSet { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            CavernPartOne = InputHelper.ParsePartOne(inputFileName);
            CavernPartTwo = InputHelper.ParsePartTwo(inputFileName);
        }

        public void SolvePartOne()
        {
            var cavern = CavernPartOne;
            var startingNode = cavern[0, 0];
            startingNode.TentativeDistance = 0;
            UnvisitedSet = new List<Node>() { startingNode };

            var destinationNode = cavern[cavern.GetLength(0) - 1, cavern.GetLength(0) - 1];

            while (!destinationNode.Visited)
            {
                Dijkstra(cavern);
            }

            Console.WriteLine($"The solution to part one is {destinationNode.TentativeDistance}.");
        }

        public void SolvePartTwo()
        {
            var cavern = CavernPartTwo;
            var startingNode = cavern[0, 0];
            startingNode.TentativeDistance = 0;
            UnvisitedSet = new List<Node>() { startingNode };

            var destinationNode = cavern[cavern.GetLength(0) - 1, cavern.GetLength(0) - 1];

            while (!destinationNode.Visited)
            {
                Dijkstra(cavern);
            }

            Console.WriteLine($"The solution to part two is {destinationNode.TentativeDistance}.");
        }

        private void Dijkstra(Node[,] cavern)
        {
            var currentNode = UnvisitedSet.OrderBy(c => c.TentativeDistance).First();

            var (i, j) = currentNode.Coordinates;

            var adjacentNodeHelper = new (int, int)[]
            {
                (-1, 0),
                (0, -1),
                (0, 1),
                (1, 0)
            };

            foreach (var node in adjacentNodeHelper)
            {
                if (i + node.Item1 < 0 ||
                    i + node.Item1 >= cavern.GetLength(0) ||
                    j + node.Item2 < 0 ||
                    j + node.Item2 >= cavern.GetLength(0))
                {
                    continue;
                }

                var adjacentNode = cavern[i + node.Item1, j + node.Item2];

                if (adjacentNode.Visited)
                {
                    continue;
                }

                adjacentNode.TentativeDistance = Math.Min(adjacentNode.TentativeDistance, currentNode.TentativeDistance + adjacentNode.Value);
                if (!UnvisitedSet.Contains(adjacentNode))
                {
                    UnvisitedSet.Add(adjacentNode);
                }
            }
            currentNode.Visited = true;
            UnvisitedSet.Remove(currentNode);
        }
    }
}
