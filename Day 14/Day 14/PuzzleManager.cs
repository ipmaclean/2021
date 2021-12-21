using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_14
{
    public class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Dictionary<string, string[]> PolymerDictionary { get; set; }
        public string InitialPolymer { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            (InitialPolymer, PolymerDictionary) = InputHelper.Parse(inputFileName);
        }

        public void Solve(int iterations, string puzzlePart)
        {
            var pairCount = new Dictionary<string, long>();

            for (int i = 0; i < InitialPolymer.Length - 1; i++)
            {
                if (pairCount.Keys.Contains(InitialPolymer.Substring(i, 2)))
                {
                    pairCount[InitialPolymer.Substring(i, 2)] ++;
                }
                else
                {
                    pairCount.Add(InitialPolymer.Substring(i, 2), 1);
                }
            }

            for (int i = 0; i < iterations; i++)
            {
                pairCount = PairInsertion(pairCount);
            }

            Console.WriteLine($"The solution to part {puzzlePart} is {CalculateSolution(pairCount)}");
        }

        private Dictionary<string, long> PairInsertion(Dictionary<string, long> pairCount)
        {
            var newPairCount = new Dictionary<string, long>();

            foreach (var oldPair in pairCount)
            {
                foreach (var newPair in PolymerDictionary[oldPair.Key])
                {
                    if (newPairCount.Keys.Contains(newPair))
                    {
                        newPairCount[newPair] += oldPair.Value;
                    }
                    else
                    {
                        newPairCount.Add(newPair, oldPair.Value);
                    }
                }
            }
            return newPairCount;
        }

        private long CalculateSolution(Dictionary<string, long> pairCount)
        {
            var solutionDictionary = new Dictionary<char, long>();

            foreach (var pair in pairCount)
            {
                if (solutionDictionary.Keys.Contains(pair.Key[0]))
                {
                    solutionDictionary[pair.Key[0]] += pair.Value;
                }
                else
                {
                    solutionDictionary.Add(pair.Key[0], pair.Value);
                }
            }

            if (solutionDictionary.Keys.Contains(InitialPolymer.Last()))
            {
                solutionDictionary[InitialPolymer.Last()]++;
            }
            else
            {
                solutionDictionary.Add(InitialPolymer.Last(), 1);
            }

            return solutionDictionary.Values.Max() - solutionDictionary.Values.Min();
        }
    }
}
