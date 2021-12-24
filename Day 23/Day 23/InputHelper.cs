using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_23
{
    public class InputHelper
    {
        public static List<Tile> ParseMaze(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var maze = new List<Tile>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var rowIndex = 0;
                while ((ln = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < ln.Length; i++)
                    {
                        if (ln[i] == ' ')
                        {
                            continue;
                        }
                        else if (ln[i] == '#')
                        {
                            var tile = new Tile((i, rowIndex), TileType.Wall);
                            maze.Add(tile);
                        }
                        else if (ln[i] == '.')
                        {
                            var tile = new Tile((i, rowIndex), TileType.Hallway);
                            maze.Add(tile);
                        }
                        else
                        {
                            var tile = new Tile((i, rowIndex), TileType.Destination);
                            maze.Add(tile);
                        }


                    }
                    rowIndex++;
                }

                var outsideDoorTiles = maze.Where(x => x.Coords == (3, 1) ||
                                                       x.Coords == (5, 1) ||
                                                       x.Coords == (7, 1) ||
                                                       x.Coords == (9, 1)).ToArray();

                foreach (var outsideDoorTile in outsideDoorTiles)
                {
                    outsideDoorTile.Type = TileType.OutsideDoor;
                }
            }
            return maze;
        }

        internal static GameState ParseInitialGameState(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var gameState = new GameState(0);
            var amphipods = new List<(int, int, char, bool)>();

            using (var sr = new StreamReader(input))
            {
                string ln;
                var rowIndex = 0;
                while ((ln = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < ln.Length; i++)
                    {
                        if (ln[i] == 'A' || ln[i] == 'B' || ln[i] == 'C' || ln[i] == 'D')
                        {
                            amphipods.Add((i, rowIndex, ln[i], false));
                        }
                    }
                    rowIndex++;
                }
            }
            gameState.PositionState = new PositionState(amphipods);
            return gameState;
        }
    }
}
