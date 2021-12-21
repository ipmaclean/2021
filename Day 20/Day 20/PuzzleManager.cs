using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_20
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public char[] EnhancementAlgorithm { get; set; }
        public bool IsFlashingImage { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            EnhancementAlgorithm = InputHelper.ParseEnhancementAlgorithm(inputFileName);
            IsFlashingImage = EnhancementAlgorithm[0] == '1';
        }

        internal void Solve(int iterations, string puzzlePart)
        {
            var image = InputHelper.ParseImage(InputFileName);

            for (int i = 0; i < iterations; i++)
            {
                image = ProcessImage(image, i);
            }

            var solution = image.SelectMany(x => x).Count(x => x == '1');

            Console.WriteLine($"The solution to part {puzzlePart} is {solution}.");
        }

        private void DoublePadImage(List<List<char>> image, int algorithmStep)
        {
            var padCharacter = '0';
            if (IsFlashingImage && algorithmStep % 2 == 1)
            {
                padCharacter = '1';
            }

            foreach (var imageRow in image)
            {
                imageRow.Insert(0, padCharacter);
                imageRow.Insert(0, padCharacter);
                imageRow.Add(padCharacter);
                imageRow.Add(padCharacter);
            }

            var padrow = new List<char>();
            for (int i = 0; i < image[0].Count; i++)
            {
                padrow.Add(padCharacter);
            }

            image.Insert(0, new List<char>(padrow));
            image.Insert(0, new List<char>(padrow));
            image.Add(new List<char>(padrow));
            image.Add(new List<char>(padrow));
        }

        private List<List<char>> ProcessImage(List<List<char>> image, int algorithmStep)
        {
            DoublePadImage(image, algorithmStep);

            var outputImage = CopyImage(image);

            for (var i = 1; i < image.Count - 1; i++)
            {
                for (var j = 1; j < image[i].Count - 1; j++)
                {
                    var algorithmIndex = FindAlgorithmIndex(image, i, j);

                    outputImage[i][j] = EnhancementAlgorithm[algorithmIndex];
                }
            }

            if (IsFlashingImage)
            {
                FlipOuterBits(outputImage);
            }
            return outputImage;
        }

        private void FlipOuterBits(List<List<char>> image)
        {
            var newCharacter = '0';
            if (image[0][0] == '0') newCharacter = '1';

            for (int i = 0; i < image[0].Count; i++)
            {
                image[0][i] = newCharacter;
            }
            for (int i = 0; i < image[0].Count; i++)
            {
                image[image.Count - 1][i] = newCharacter;
            }
            for (int i = 1; i < image[0].Count - 1; i++)
            {
                image[i][0] = newCharacter;
                image[i][image[i].Count - 1] = newCharacter;
            }
        }

        private List<List<char>> CopyImage(List<List<char>> image)
        {
            var copy = new List<List<char>>();
            foreach (var imageRow in image)
            {
                copy.Add(new List<char>(imageRow));
            }
            return copy;
        }

        private int FindAlgorithmIndex(List<List<char>> image, int i, int j)
        {
            var binaryNumber = string.Empty;
            var binaryNumberHelper = new (int, int)[]
            {
                (-1, -1),
                (-1, 0),
                (-1, 1),
                (0, -1),
                (0, 0),
                (0, 1),
                (1, -1),
                (1, 0),
                (1, 1),

            };
            foreach (var helper in binaryNumberHelper)
            {
                binaryNumber += image[i + helper.Item1][j + helper.Item2];
            }
            return Convert.ToInt32(binaryNumber, 2);
        }
    }
}
