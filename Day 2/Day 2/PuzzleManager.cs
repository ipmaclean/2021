using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2
{
    class PuzzleManager
    {
        public List<Instruction> Instructions { get; set; }

        public PuzzleManager(string fileName)
        {
            Instructions = InputHelper.Parse(fileName);
        }

        internal void SolvePartOne()
        {
            var horizontalPosition = 0;
            var depth = 0;

            foreach (var instruction in Instructions)
            {
                if (instruction.Command == "forward")
                {
                    horizontalPosition += instruction.Value;
                }
                else if (instruction.Command == "up")
                {
                    depth -= instruction.Value;
                }
                else if (instruction.Command == "down")
                {
                    depth += instruction.Value;
                }
            }
            Console.WriteLine($"The solution to part one is {horizontalPosition * depth}.");
        }

        internal void SolvePartTwo()
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in Instructions)
            {
                if (instruction.Command == "forward")
                {
                    horizontalPosition += instruction.Value;
                    depth += aim * instruction.Value;
                }
                else if (instruction.Command == "up")
                {
                    aim -= instruction.Value;
                }
                else if (instruction.Command == "down")
                {
                    aim += instruction.Value;
                }
            }
            Console.WriteLine($"The solution to part one is {horizontalPosition * depth}.");
        }
    }
}
