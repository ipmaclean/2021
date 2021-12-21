using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_12
{
    public class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Cave[] Caves { get; set; }
        public List<List<Cave>> Routes { get; set; } = new List<List<Cave>>();

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Caves = InputHelper.Parse(inputFileName);
        }

        public void SolvePartOne()
        {
            var startCave = Caves.First(c => c.Name == "start");
            var routesToCheck = new Queue<RouteToCheck>();
            routesToCheck.Enqueue(new RouteToCheck(startCave, new List<Cave>(), false));

            while (routesToCheck.Count > 0)
            {
                QueueAdjacentCavesPartOne(routesToCheck);
            }
            Console.WriteLine($"The solution to part one is {Routes.Count}.");
        }

        private void QueueAdjacentCavesPartOne(Queue<RouteToCheck> routesToCheck)
        {
            var routeToCheck = routesToCheck.Dequeue();
            if (routeToCheck.Cave.Name == "end")
            {
                var fullRoute = routeToCheck.Route.GetRange(0, routeToCheck.Route.Count);
                Routes.Add(fullRoute);
                return;
            }
            foreach (var adjacentCave in routeToCheck.Cave.AdjacentCaves)
            {
                if (routeToCheck.Route.Contains(adjacentCave) && adjacentCave.IsSmall)
                {
                    continue;
                }
                routesToCheck.Enqueue(new RouteToCheck(adjacentCave, routeToCheck.Route, routeToCheck.SmallCaveRevisted));
            }
        }

        public void SolvePartTwo()
        {
            Routes = new List<List<Cave>>();
            var startCave = Caves.First(c => c.Name == "start");
            var routesToCheck = new Queue<RouteToCheck>();
            routesToCheck.Enqueue(new RouteToCheck(startCave, new List<Cave>(), false));

            while (routesToCheck.Count > 0)
            {
                QueueAdjacentCavesPartTwo(routesToCheck);
            }
            Console.WriteLine($"The solution to part two is {Routes.Count}.");
        }

        private void QueueAdjacentCavesPartTwo(Queue<RouteToCheck> routesToCheck)
        {
            var routeToCheck = routesToCheck.Dequeue();
            if (routeToCheck.Cave.Name == "end")
            {
                var fullRoute = routeToCheck.Route.GetRange(0, routeToCheck.Route.Count);
                Routes.Add(fullRoute);
                return;
            }
            foreach (var adjacentCave in routeToCheck.Cave.AdjacentCaves)
            {
                if (routeToCheck.Route.Contains(adjacentCave) && adjacentCave.IsSmall)
                {
                    if (!routeToCheck.SmallCaveRevisted && adjacentCave.Name != "start")
                    {
                        routesToCheck.Enqueue(new RouteToCheck(adjacentCave, routeToCheck.Route, true));
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                routesToCheck.Enqueue(new RouteToCheck(adjacentCave, routeToCheck.Route, routeToCheck.SmallCaveRevisted));
            }
        }
    }
}
