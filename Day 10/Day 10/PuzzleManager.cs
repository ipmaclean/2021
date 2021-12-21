using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_10
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Dictionary<char, long> ScoresPartOne { get; set; }
        public Dictionary<char, long> ScoresPartTwo { get; set; }
        public char[] OpenBrackets { get; set; } = new char[] { '(', '[', '{', '<' };
        public char[] CloseBrackets { get; set; } = new char[] { ')', ']', '}', '>' };
        public Dictionary<char, char> MatchingBrackets { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;

            ScoresPartOne = new Dictionary<char, long>()
            {
                { ')' , 3 },
                { ']' , 57 },
                { '}' , 1197 },
                { '>' , 25137 }
            };

            MatchingBrackets = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };

            ScoresPartTwo = new Dictionary<char, long>()
            {
                { ')' , 1 },
                { ']' , 2 },
                { '}' , 3 },
                { '>' , 4 }
            };
        }

        public void SolvePartOne()
        {
            var subsystem = InputHelper.Parse(InputFileName);
            long solution = 0;
            foreach (var line in subsystem)
            {
                solution += ProcessLinePartOne(line);
            }
            Console.WriteLine($"The solution to part one is {solution}.");
        }

        private long ProcessLinePartOne(Queue<char> line)
        {
            var stack = new Stack<char>();

            while (line.Count > 0)
            {
                if (OpenBrackets.Contains(line.Peek()))
                {
                    stack.Push(line.Dequeue());
                }
                else if (CloseBrackets.Contains(line.Peek()))
                {
                    if (MatchingBrackets[stack.Peek()] == line.Peek())
                    {
                        stack.Pop();
                        line.Dequeue();
                    }
                    else
                    {
                        return ScoresPartOne[line.Peek()];
                    }
                }
            }
            return 0;
        }

        public void SolvePartTwo()
        {
            var subsystem = InputHelper.Parse(InputFileName);
            var scores = new List<long>();
            foreach (var line in subsystem)
            {
                var score = ProcessLinePartTwo(line);
                if (score > 0)
                {
                    scores.Add(score);
                }
            }

            scores = scores.OrderBy(x => x).ToList();

            while (scores.Count != 1)
            {
                scores.RemoveAt(0);
                scores.RemoveAt(scores.Count - 1);
            }

            Console.WriteLine($"The solution to part two is {scores[0]}.");
        }

        private long ProcessLinePartTwo(Queue<char> line)
        {
            var stack = new Stack<char>();

            while (line.Count > 0)
            {
                if (OpenBrackets.Contains(line.Peek()))
                {
                    stack.Push(line.Dequeue());
                }
                else if (CloseBrackets.Contains(line.Peek()))
                {
                    if (MatchingBrackets[stack.Peek()] == line.Peek())
                    {
                        stack.Pop();
                        line.Dequeue();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            long score = 0;
            while (stack.Count > 0)
            {
                score *= 5;
                var closingBracketNeeded = MatchingBrackets[stack.Pop()];
                score += ScoresPartTwo[closingBracketNeeded];
            }
            return score;
        }
    }
}
