using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_18
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<string> InputStrings { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            InputStrings = InputHelper.Parse(inputFileName);
        }

        public void SolvePartOne()
        {
            var currentLine = InputStrings.First();
            var inputQueue = new Queue<string>();

            for (int i = 1; i < InputStrings.Count; i++)
            {
                var sum = "[" + currentLine + "," + InputStrings[i] + "]";
                inputQueue = CreateInputQueue(sum);
                var didExplode = false;

                // Reduce the sum - explode then split
                var continueReducing = true;
                while (continueReducing)
                {
                    (didExplode, inputQueue) = TryExplode(inputQueue);
                    if (didExplode)
                    {
                        continue;
                    }
                    (continueReducing, inputQueue) = TrySplit(inputQueue);
                }

                currentLine = QueueToString(inputQueue);
            }
            var solutionQueue = CreateInputQueue(currentLine);
            var solution = CountMagnitude(solutionQueue);

            Console.WriteLine($"The solution to part one is {solution}.");
        }

        public void SolvePartTwo()
        {
            var currentLine = InputStrings.First();
            var inputQueue = new Queue<string>();
            long solution = 0;

            for (int i = 0; i < InputStrings.Count; i++)
            {
                for (int j = 0; j < InputStrings.Count; j++)
                {
                    if (i == j) continue;

                    var sum = "[" + InputStrings[i] + "," + InputStrings[j] + "]";
                    inputQueue = CreateInputQueue(sum);
                    var didExplode = false;

                    var continueReducing = true;
                    while (continueReducing)
                    {
                        (didExplode, inputQueue) = TryExplode(inputQueue);
                        if (didExplode)
                        {
                            continue;
                        }
                        (continueReducing, inputQueue) = TrySplit(inputQueue);
                    }

                    var magnitude = CountMagnitude(inputQueue);
                    solution = Math.Max(magnitude, solution);
                }
            }
            Console.WriteLine($"The solution to part two is {solution}.");
        }

        private Queue<string> CreateInputQueue(string sum)
        {
            var inputQueue = new Queue<string>();

            foreach (var character in sum)
            {
                if (character == ',') continue;
                inputQueue.Enqueue(character.ToString());
            }
            return inputQueue;
        }

        private string QueueToString(Queue<string> inputQueue)
        {
            var sb = new StringBuilder();

            while (inputQueue.Count > 0)
            {
                sb.Append(inputQueue.Dequeue());
            }
            return sb.ToString();
        }

        private (bool, Queue<string>) TryExplode(Queue<string> inputQueue)
        {
            var outputList = new List<string>();
            var bracketCount = 0;
            var hasExploded = false;
            var valueToAddToNextNumber = 0;

            while (inputQueue.Count > 0)
            {
                var nextEntry = inputQueue.Dequeue();

                if (nextEntry == "[")
                {
                    bracketCount++;

                    // If there's 5 in the bracketCount it's time to explode
                    if (bracketCount > 4 && !hasExploded)
                    {
                        valueToAddToNextNumber = Explode(inputQueue, outputList);
                        hasExploded = true;
                    }
                    else
                    {
                        outputList.Add(nextEntry);
                    }
                }
                else if (nextEntry == "]")
                {
                    bracketCount--;
                    outputList.Add(nextEntry);
                }
                else
                {
                    if (valueToAddToNextNumber != 0)
                    {
                        var newNumber = int.Parse(nextEntry) + valueToAddToNextNumber;
                        outputList.Add(newNumber.ToString());
                        valueToAddToNextNumber = 0;
                    }
                    else
                    {
                        outputList.Add(nextEntry);
                    }

                }
            }

            inputQueue = new Queue<string>();
            foreach (var part in outputList)
            {
                inputQueue.Enqueue(part);
            }
            return (hasExploded, inputQueue);
        }

        private int Explode(Queue<string> inputQueue,
                             List<string> outputList)
        {
            var firstNumberOfExplodePair = int.Parse(inputQueue.Dequeue());

            // Add the first value of exploding pair to the closest left number
            for (int i = outputList.Count - 1; i >= 0; i--)
            {
                if (int.TryParse(outputList[i], out var value))
                {
                    outputList[i] = (firstNumberOfExplodePair + value).ToString();
                    break;
                }
            }

            var secondNumberOfExplodePair = inputQueue.Dequeue();
            inputQueue.Dequeue();

            // Replace the pair with 0 in the output.
            outputList.Add("0");

            // Return the second value of exploding pair so it can be added to the next number found
            return int.Parse(secondNumberOfExplodePair);
        }

        private (bool, Queue<string>) TrySplit(Queue<string> inputQueue)
        {
            var outputQueue = new Queue<string>();
            var hasSplit = false;

            while (inputQueue.Count > 0)
            {
                var nextEntry = inputQueue.Dequeue();
                if (!hasSplit && int.TryParse(nextEntry, out var value) && value > 9)
                {
                    outputQueue.Enqueue("[");
                    var firstNumber = (int)Math.Floor((decimal)value / 2);
                    outputQueue.Enqueue(firstNumber.ToString());
                    var secondNumber = (int)Math.Ceiling((decimal)value / 2);
                    outputQueue.Enqueue(secondNumber.ToString());
                    outputQueue.Enqueue("]");
                    hasSplit = true;
                }
                else
                {
                    outputQueue.Enqueue(nextEntry);
                }
            }
            return (hasSplit, outputQueue);
        }

        private long CountMagnitude(Queue<string> inputQueue)
        {
            long solution = 0;
            var nextEntry = inputQueue.Dequeue();
            if (nextEntry == "[")
            {
                solution += MagnitudeOfPair(inputQueue);
            }
            return solution;
        }

        private long MagnitudeOfPair(Queue<string> inputQueue)
        {
            long solution = 0;
            var firstEntry = inputQueue.Dequeue();
            if (int.TryParse(firstEntry, out var value1))
            {
                solution += 3 * value1;
            }
            else
            {
                solution += 3 * MagnitudeOfPair(inputQueue);
            }
            var secondEntry = inputQueue.Dequeue();
            if (int.TryParse(secondEntry, out var value2))
            {
                solution += 2 * value2;
                inputQueue.Dequeue();
            }
            else
            {
                solution += 2 * MagnitudeOfPair(inputQueue);
                inputQueue.Dequeue();
            }
            return solution;
        }
    }
}
