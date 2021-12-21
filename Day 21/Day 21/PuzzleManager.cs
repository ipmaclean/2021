using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_21
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public (int, int) StartingPositions { get; set; }
        public Dictionary<int, long> WorldsDictionary { get; set; }
        public Dictionary<(int, int, int, int, int), (long, long)> Memoisation { get; set; } = new Dictionary<(int, int, int, int, int), (long, long)>();

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            StartingPositions = InputHelper.Parse(inputFileName);
            WorldsDictionary = new Dictionary<int, long>()
            {
                { 3, 1 },
                { 4, 3 },
                { 5, 6 },
                { 6, 7 },
                { 7, 6 },
                { 8, 3 },
                { 9, 1 }
            };
        }

        internal void SolvePartOne()
        {
            var playerOne = new Player(1, StartingPositions.Item1);
            var playerTwo = new Player(2, StartingPositions.Item2);
            var die = new DeterministicDie();

            while (true)
            {
                playerOne.AdvancePosition(die.RollXTimes(3));
                if (playerOne.Score >= 1000)
                {
                    //Console.WriteLine("Player one wins the practice game!");
                    Console.WriteLine($"The solution to part one is {playerTwo.Score * die.TimesRolled}.");
                    break;
                }
                playerTwo.AdvancePosition(die.RollXTimes(3));
                if (playerTwo.Score >= 1000)
                {
                    //Console.WriteLine("Player two wins the practice game!");
                    Console.WriteLine($"The solution to part one is {playerOne.Score * die.TimesRolled}.");
                    break;
                }
            }
        }

        internal void SolvePartTwo()
        {
            (long, long) scores = (0, 0);

            foreach (var keyValue in WorldsDictionary)
            {
                var worldsWon = PlayTurn(keyValue.Key,
                                         1,
                                         0, StartingPositions.Item1,
                                         0, StartingPositions.Item2);
                scores.Item1 += keyValue.Value * worldsWon.Item1;
                scores.Item2 += keyValue.Value * worldsWon.Item2;
            }
            Console.WriteLine($"The solution to part two is {Math.Max(scores.Item1, scores.Item2)}.");
        }

        private (long, long) PlayTurn(int rollValue,
                                      int playerNumber,
                                      int playerOneScore, int playerOnePosition,
                                      int playerTwoScore, int playerTwoPosition)
        {
            if (playerNumber == 1)
            {
                playerOnePosition--;
                playerOnePosition = (playerOnePosition + rollValue) % 10;
                playerOnePosition++;
                playerOneScore += playerOnePosition;
                playerNumber = 2;

                if (playerOneScore >= 21)
                {
                    return (1, 0);
                }
            }
            else
            {
                playerTwoPosition--;
                playerTwoPosition = (playerTwoPosition + rollValue) % 10;
                playerTwoPosition++;
                playerTwoScore += playerTwoPosition;
                playerNumber = 1;

                if (playerTwoScore >= 21)
                {
                    return (0, 1);
                }
            }
            var gameState = (playerNumber,
                             playerOneScore, playerOnePosition,
                             playerTwoScore, playerTwoPosition);

            if (Memoisation.ContainsKey(gameState))
            {
                return Memoisation[gameState];
            }
            else
            {
                (long, long) scores = (0, 0);
                foreach (var keyValue in WorldsDictionary)
                {

                    var worldsWon = PlayTurn(keyValue.Key,
                                             playerNumber,
                                             playerOneScore, playerOnePosition,
                                             playerTwoScore, playerTwoPosition);
                    scores.Item1 += keyValue.Value * worldsWon.Item1;
                    scores.Item2 += keyValue.Value * worldsWon.Item2;
                }
                Memoisation[gameState] = scores;
                return scores;
            }
        }
    }
}
