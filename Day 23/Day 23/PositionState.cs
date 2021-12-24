using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_23
{
    class PositionState
    {
        // coord1, coord2, type of amphipod, isInDestination
        public List<(int, int, char, bool)> Amphipods { get; set; }

        public PositionState(List<(int, int, char, bool)> amphipods)
        {
            Amphipods = new List<(int, int, char, bool)>(amphipods);
        }

        internal bool IsPath((int, int) coords1, (int, int) coords2, List<Tile> maze)
        {
            var firstTile = maze.First(x => x.Coords == coords1);
            var secondTile = maze.First(x => x.Coords == coords2);
            // if both destination
            if (firstTile.Type == TileType.Destination && secondTile.Type == TileType.Destination)
            {
                var path = new List<(int, int)>();
                foreach (var value in Enumerable.Range(2, coords1.Item2 - 2))
                {
                    path.Add((coords1.Item1, value));
                }
                foreach (var value in Enumerable.Range(Math.Min(coords1.Item1, coords2.Item1), Math.Abs(coords1.Item1 - coords2.Item1) + 1))
                {
                    path.Add((value, 1));
                }
                foreach (var value in Enumerable.Range(2, coords2.Item2 - 1))
                {
                    path.Add((coords2.Item1, value));
                }

                foreach (var amphipod in Amphipods)
                {
                    if (path.Contains((amphipod.Item1, amphipod.Item2)))
                    {
                        return false;
                    }
                }
                return true;
            }
            // if first is destination and second is hallway
            else if (firstTile.Type == TileType.Destination && secondTile.Type == TileType.Hallway)
            {
                var path = new List<(int, int)>();
                foreach (var value in Enumerable.Range(2, coords1.Item2 - 2))
                {
                    path.Add((coords1.Item1, value));
                }
                foreach (var value in Enumerable.Range(Math.Min(coords1.Item1, coords2.Item1), Math.Abs(coords1.Item1 - coords2.Item1) + 1))
                {
                    path.Add((value, 1));
                }

                foreach (var amphipod in Amphipods)
                {
                    if (path.Contains((amphipod.Item1, amphipod.Item2)))
                    {
                        return false;
                    }
                }
                return true;
            }
            // if first is hallway and second is destination
            else if (firstTile.Type == TileType.Hallway && secondTile.Type == TileType.Destination)
            {
                var path = new List<(int, int)>();
                foreach (var value in Enumerable.Range(Math.Min(coords1.Item1, coords2.Item1), Math.Abs(coords1.Item1 - coords2.Item1) + 1))
                {
                    path.Add((value, 1));
                }
                path.Remove((coords1.Item1, coords1.Item2));
                foreach (var value in Enumerable.Range(2, coords2.Item2 - 1))
                {
                    path.Add((coords2.Item1, value));
                }

                foreach (var amphipod in Amphipods)
                {
                    if (path.Contains((amphipod.Item1, amphipod.Item2)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                throw new Exception("Bad path");
            }
        }

        internal int PathLength((int, int) coords1, (int, int) coords2, List<Tile> maze)
        {
            var firstTile = maze.First(x => x.Coords == coords1);
            var secondTile = maze.First(x => x.Coords == coords2);
            var pathlength = 0;
            // if both destination
            if (firstTile.Type == TileType.Destination && secondTile.Type == TileType.Destination)
            {
                pathlength += coords1.Item2 - 1;
                pathlength += Math.Abs(coords1.Item1 - coords2.Item1);
                pathlength += coords2.Item2 - 1;
            }
            // if first is destination and second is hallway
            else if (firstTile.Type == TileType.Destination && secondTile.Type == TileType.Hallway)
            {
                pathlength += coords1.Item2 - 1;
                pathlength += Math.Abs(coords1.Item1 - coords2.Item1);
            }
            // if first is hallway and second is destination
            else if (firstTile.Type == TileType.Hallway && secondTile.Type == TileType.Destination)
            {
                pathlength += Math.Abs(coords1.Item1 - coords2.Item1);
                pathlength += coords2.Item2 - 1;
            }
            else
            {
                throw new Exception("Bad path");
            }
            return pathlength;
        }

        public string GetHash()
        {
            var hash = string.Empty;

            var positionState = Amphipods.Select(x => (x.Item1, x.Item2, x.Item3)).OrderBy(x => x.Item1).ThenBy(x => x.Item2).ThenBy(x => x.Item3).ToList();

            foreach (var entry in positionState)
            {
                hash += entry.Item1.ToString() + entry.Item2.ToString() + entry.Item3.ToString();
            }

            return hash;
        }
    }
}
