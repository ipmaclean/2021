using System.IO;

namespace Day_15
{
    public static class InputHelper
    {
        public static Node[,] ParsePartOne(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var cavern = new Node[0, 0];

            using (var sr = new StreamReader(input))
            {
                string ln = sr.ReadLine();
                var sideLength = ln.Length;
                cavern = new Node[sideLength, sideLength];
                var rowIndex = 0;
                do
                {
                    for (int i = 0; i < sideLength; i++)
                    {
                        cavern[i, rowIndex] = new Node(ln[i] - '0', (i, rowIndex));
                    }
                    rowIndex++;
                }
                while ((ln = sr.ReadLine()) != null);
            }
            return cavern;
        }

        public static Node[,] ParsePartTwo(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var cavern = new Node[0, 0];

            using (var sr = new StreamReader(input))
            {
                string ln = sr.ReadLine();
                var sideLength = ln.Length;
                cavern = new Node[sideLength * 5, sideLength * 5];
                var rowIndex = 0;
                do
                {
                    for (int i = 0; i < sideLength; i++)
                    {
                        for (int rowRepeater = 0; rowRepeater < sideLength * 5; rowRepeater += sideLength)
                        {
                            for (int columnRepeater = 0; columnRepeater < sideLength * 5; columnRepeater += sideLength)
                            {
                                var valueForNode = (((ln[i] - '0') + ((rowRepeater + columnRepeater) / sideLength) - 1) % 9) + 1;
                                cavern[i + rowRepeater, rowIndex + columnRepeater] = new Node(valueForNode, (i + rowRepeater, rowIndex + columnRepeater));
                            }
                        }
                    }
                    rowIndex++;
                }
                while ((ln = sr.ReadLine()) != null);
            }
            return cavern;
        }
    }
}
