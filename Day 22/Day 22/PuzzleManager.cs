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

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        public void Solve(string puzzlePart)
        {
            var instructions = InputHelper.Parse(InputFileName);
            var cuboidVolumeDictionary = new Dictionary<Cuboid, long>();
            foreach (var instruction in instructions)
            {
                if (puzzlePart == "one" && (Math.Abs(instruction.Cuboid.Vertex1.Item1) > 50 || Math.Abs(instruction.Cuboid.Vertex2.Item1) > 50)) continue;

                var newCuboidVolumeDictionary = new Dictionary<Cuboid, long>();
                foreach (var cuboidVolume in cuboidVolumeDictionary)
                {
                    // If no overlap continue;
                    if (Math.Max(Math.Min(instruction.Cuboid.Vertex2.Item1, cuboidVolume.Key.Vertex2.Item1) - Math.Max(instruction.Cuboid.Vertex1.Item1, cuboidVolume.Key.Vertex1.Item1) + 1, 0) == 0 ||
                        Math.Max(Math.Min(instruction.Cuboid.Vertex2.Item2, cuboidVolume.Key.Vertex2.Item2) - Math.Max(instruction.Cuboid.Vertex1.Item2, cuboidVolume.Key.Vertex1.Item2) + 1, 0) == 0 ||
                        Math.Max(Math.Min(instruction.Cuboid.Vertex2.Item3, cuboidVolume.Key.Vertex2.Item3) - Math.Max(instruction.Cuboid.Vertex1.Item3, cuboidVolume.Key.Vertex1.Item3) + 1, 0) == 0)
                    {
                        continue;
                    }

                    var overlapCuboid = new Cuboid((Math.Max(instruction.Cuboid.Vertex1.Item1, cuboidVolume.Key.Vertex1.Item1), Math.Max(instruction.Cuboid.Vertex1.Item2, cuboidVolume.Key.Vertex1.Item2), Math.Max(instruction.Cuboid.Vertex1.Item3, cuboidVolume.Key.Vertex1.Item3)),
                                                   (Math.Min(instruction.Cuboid.Vertex2.Item1, cuboidVolume.Key.Vertex2.Item1), Math.Min(instruction.Cuboid.Vertex2.Item2, cuboidVolume.Key.Vertex2.Item2), Math.Min(instruction.Cuboid.Vertex2.Item3, cuboidVolume.Key.Vertex2.Item3)));

                    if (cuboidVolume.Value > 0)
                    {
                        newCuboidVolumeDictionary.Add(overlapCuboid, -overlapCuboid.CalculateVolume());
                    }
                    else if (cuboidVolume.Value < 0)
                    {
                        newCuboidVolumeDictionary.Add(overlapCuboid, overlapCuboid.CalculateVolume());
                    }
                }

                if (instruction.Name == "on")
                {
                    newCuboidVolumeDictionary.Add(instruction.Cuboid, instruction.Cuboid.CalculateVolume());
                }

                foreach (var cuboidVolume in newCuboidVolumeDictionary)
                {
                    if (cuboidVolumeDictionary.ContainsKey(cuboidVolume.Key))
                    {
                        if (cuboidVolumeDictionary[cuboidVolume.Key] + cuboidVolume.Value == 0)
                        {
                            cuboidVolumeDictionary.Remove(cuboidVolume.Key);
                        }
                        else
                        {
                            cuboidVolumeDictionary[cuboidVolume.Key] += cuboidVolume.Value;
                        }
                    }
                    else
                    {
                        cuboidVolumeDictionary.Add(cuboidVolume.Key, cuboidVolume.Value);
                    }
                }
            }

            long solution = 0;
            foreach (var volume in cuboidVolumeDictionary.Values)
            {
                solution += volume;
            }
            Console.WriteLine($"The solution to part {puzzlePart} is {solution}.");
        }
    }
}
