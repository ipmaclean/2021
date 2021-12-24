using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_23
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<Tile> Maze { get; set; }
        public Dictionary<char, int> CostDictionary { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Maze = InputHelper.ParseMaze(inputFileName);
            CostDictionary = new Dictionary<char, int>()
            {
                { 'A' , 1 },
                { 'B' , 10 },
                { 'C' , 100 },
                { 'D' , 1000 }
            };
        }

        public void Solve(string puzzlePart)
        {
            var initialGameState = InputHelper.ParseInitialGameState(InputFileName);

            var finalDestinationDictionary = new Dictionary<char, (int, int)>();
            if (puzzlePart == "one")
            {
                finalDestinationDictionary = new Dictionary<char, (int, int)>()
                    {
                        {'A', (3, 3) },
                        {'B', (5, 3) },
                        {'C', (7, 3) },
                        {'D', (9, 3) }
                    };
            }
            else
            {
                finalDestinationDictionary = new Dictionary<char, (int, int)>()
                    {
                        {'A', (3, 5) },
                        {'B', (5, 5) },
                        {'C', (7, 5) },
                        {'D', (9, 5) }
                    };
            }

            foreach (var finalDestination in finalDestinationDictionary)
            {
                var alreadyInFinalDestination = initialGameState.PositionState.Amphipods.FirstOrDefault(x => x.Item3 == finalDestination.Key && (x.Item1, x.Item2) == finalDestination.Value);
                if (alreadyInFinalDestination.Item1 != 0)
                {
                    initialGameState.PositionState.Amphipods.Remove(alreadyInFinalDestination);
                    initialGameState.PositionState.Amphipods.Add((alreadyInFinalDestination.Item1, alreadyInFinalDestination.Item2, alreadyInFinalDestination.Item3, true));
                    finalDestinationDictionary[finalDestination.Key] = (finalDestination.Value.Item1, finalDestination.Value.Item2 - 1);
                }
            }

            initialGameState.FinalDestinationDictionary = finalDestinationDictionary;

            var visitedStates = new HashSet<string>();
            var statesToCheck = new List<GameState>();
            statesToCheck.Add(initialGameState);

            var solution = 0;

            while (statesToCheck.Count > 0)
            {
                // Current state is the state with the lowest score
                var currentState = statesToCheck.Aggregate((curMin, x) => (curMin == null || x.Score < curMin.Score ? x : curMin));
                statesToCheck.Remove(currentState);

                // Check current against visited states and discard if already visited
                if (visitedStates.Contains(currentState.PositionState.GetHash()))
                {
                    continue;
                }

                // Add to visited states
                visitedStates.Add(currentState.PositionState.GetHash());

                // Check to see if this state is solved - break while loop if so
                if (currentState.FinalDestinationDictionary.Count() == 0)
                {
                    solution = currentState.Score;
                    break;
                }

                // Look for amphipods that can go to final destination
                var amphipodMovedToFinalDestination = false;
                foreach (var finalDestination in currentState.FinalDestinationDictionary)
                {
                    if (currentState.PositionState.Amphipods.Any(x => x.Item1 == finalDestination.Value.Item1 && x.Item2 == finalDestination.Value.Item2))
                    {
                        continue;
                    }

                    foreach (var amphipod in currentState.PositionState.Amphipods.Where(x => x.Item3 == finalDestination.Key).ToList())
                    {
                        if (amphipod.Item4)
                        {
                            continue;
                        }

                        if (currentState.PositionState.IsPath((amphipod.Item1, amphipod.Item2), (finalDestination.Value.Item1, finalDestination.Value.Item2), Maze))
                        {
                            // Queue the next state (with change of final destination in dictionary and of position state) and move on to next state
                            var newScore = currentState.Score + (currentState.PositionState.PathLength((amphipod.Item1, amphipod.Item2), (finalDestination.Value.Item1, finalDestination.Value.Item2), Maze) * CostDictionary[amphipod.Item3]);
                            var nextState = new GameState(newScore, currentState.FinalDestinationDictionary);
                            nextState.PositionState = new PositionState(currentState.PositionState.Amphipods);
                            nextState.PositionState.Amphipods.Remove(amphipod);
                            nextState.PositionState.Amphipods.Add((finalDestination.Value.Item1, finalDestination.Value.Item2, amphipod.Item3, true));
                            nextState.FinalDestinationDictionary[amphipod.Item3] = (nextState.FinalDestinationDictionary[amphipod.Item3].Item1, nextState.FinalDestinationDictionary[amphipod.Item3].Item2 - 1);
                            if (nextState.FinalDestinationDictionary[amphipod.Item3].Item2 == 1)
                            {
                                nextState.FinalDestinationDictionary.Remove(amphipod.Item3);
                            }
                            statesToCheck.Add(nextState);

                            amphipodMovedToFinalDestination = true;
                            break;
                        }
                    }
                    if (amphipodMovedToFinalDestination)
                    {
                        break;
                    }
                }
                if (amphipodMovedToFinalDestination)
                {
                    continue;
                }

                // Move each amphipod not already in a hallway to hallways if possible, queueing each state after ordering by score
                var statesToEnqueue = new List<GameState>();
                foreach (var amphipod in currentState.PositionState.Amphipods)
                {
                    if (amphipod.Item4)
                    {
                        continue;
                    }

                    var currentAmphipodTile = Maze.First(x => x.Coords == (amphipod.Item1, amphipod.Item2));
                    if (currentAmphipodTile.Type == TileType.Hallway)
                    {
                        continue;
                    }

                    foreach (var hallway in Maze.Where(x => x.Type == TileType.Hallway).ToList())
                    {
                        if (currentState.PositionState.IsPath((amphipod.Item1, amphipod.Item2), (hallway.Coords.Item1, hallway.Coords.Item2), Maze))
                        {
                            // Queue next state where an amphipod has moved to a hallway
                            var newScore = currentState.Score + (currentState.PositionState.PathLength((amphipod.Item1, amphipod.Item2), (hallway.Coords.Item1, hallway.Coords.Item2), Maze) * CostDictionary[amphipod.Item3]);
                            var nextState = new GameState(newScore, currentState.FinalDestinationDictionary);
                            nextState.PositionState = new PositionState(currentState.PositionState.Amphipods);
                            nextState.PositionState.Amphipods.Remove(amphipod);
                            nextState.PositionState.Amphipods.Add((hallway.Coords.Item1, hallway.Coords.Item2, amphipod.Item3, false));

                            if (!visitedStates.Contains(nextState.PositionState.GetHash()))
                            {
                            statesToEnqueue.Add(nextState);
                            }
                        }
                    }
                }
                statesToEnqueue = statesToEnqueue.OrderBy(x => x.Score).ToList();
                foreach (var stateToEnqueue in statesToEnqueue)
                {
                    statesToCheck.Add(stateToEnqueue);
                }
            }
            Console.WriteLine($"The solution to part {puzzlePart} is {solution}.");
        }
    }
}