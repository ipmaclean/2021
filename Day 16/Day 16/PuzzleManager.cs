using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_16
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public Queue<char> Input { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        public void Solve()
        {
            var input = InputHelper.Parse(InputFileName);
            long partOneSolution = 0;

            var partTwoSolution = ParsePacket(input, ref partOneSolution);

            Console.WriteLine($"The solution to part one is {partOneSolution}.");
            Console.WriteLine($"The solution to part two is {partTwoSolution}.");
        }

        private long ParsePacket(Queue<char> input, ref long partOneSolution)
        {
            var packetVersion = ReadDigits(input, 3);
            partOneSolution += packetVersion;
            var typeId = (PacketType)ReadDigits(input, 3);

            if (typeId == PacketType.Literal)
            {
                return ReadLiteralValue(input);
            }
            else
            {
                return ReadOperator(input, typeId, ref partOneSolution);
            }
        }

        private long ReadOperator(Queue<char> input, PacketType typeId, ref long partOneSolution)
        {
            var lengthTypeId = input.Dequeue() - '0';

            long countOfNextBitsOrSubPackets = 0;
            var lengthInBits = false;
            var lengthInSubPackets = false;

            if (lengthTypeId == 0)
            {
                countOfNextBitsOrSubPackets = ReadDigits(input, 15);
                lengthInBits = true;
            }
            else
            {
                countOfNextBitsOrSubPackets = ReadDigits(input, 11);
                lengthInSubPackets = true;
            }

            switch (typeId)
            {
                case PacketType.Sum:
                    long sum = 0;
                    if (lengthInSubPackets)
                    {
                        for (int i = 0; i < countOfNextBitsOrSubPackets; i++)
                        {
                            sum += ParsePacket(input, ref partOneSolution);
                        }
                    }
                    else if (lengthInBits)
                    {
                        var inputLengthAfterCountingBits = input.Count - countOfNextBitsOrSubPackets;
                        while (input.Count > inputLengthAfterCountingBits)
                        {
                            sum += ParsePacket(input, ref partOneSolution);
                        }
                    }
                    return sum;
                case PacketType.Product:
                    long product = 1;
                    if (lengthInSubPackets)
                    {
                        for (int i = 0; i < countOfNextBitsOrSubPackets; i++)
                        {
                            product *= ParsePacket(input, ref partOneSolution);
                        }
                    }
                    else if (lengthInBits)
                    {
                        var inputLengthAfterCountingBits = input.Count - countOfNextBitsOrSubPackets;
                        
                        while (input.Count > inputLengthAfterCountingBits)
                        {
                            product *= ParsePacket(input, ref partOneSolution);
                        }
                    }
                    return product;
                case PacketType.Minimum:
                    long minimum = long.MaxValue;
                    if (lengthInSubPackets)
                    {
                        for (int i = 0; i < countOfNextBitsOrSubPackets; i++)
                        {
                            minimum = Math.Min(ParsePacket(input, ref partOneSolution), minimum);
                        }
                    }
                    else if (lengthInBits)
                    {
                        var inputLengthAfterCountingBits = input.Count - countOfNextBitsOrSubPackets;
                        while (input.Count > inputLengthAfterCountingBits)
                        {
                            minimum = Math.Min(ParsePacket(input, ref partOneSolution), minimum);
                        }                        
                    }
                    return minimum;
                case PacketType.Maximum:
                    long maximum = 0;
                    if (lengthInSubPackets)
                    {
                        for (int i = 0; i < countOfNextBitsOrSubPackets; i++)
                        {
                            maximum = Math.Max(ParsePacket(input, ref partOneSolution), maximum);
                        }
                    }
                    else if (lengthInBits)
                    {
                        var inputLengthAfterCountingBits = input.Count - countOfNextBitsOrSubPackets;
                        while (input.Count > inputLengthAfterCountingBits)
                        {
                            maximum = Math.Max(ParsePacket(input, ref partOneSolution), maximum);
                        }
                    }
                    return maximum;
                case PacketType.GreaterThan:
                    var firstGTSubPacketValue = ParsePacket(input, ref partOneSolution);
                    var secondGTSubPacketValue = ParsePacket(input, ref partOneSolution);
                    if (firstGTSubPacketValue > secondGTSubPacketValue) return 1;
                    else return 0;
                case PacketType.LessThan:
                    var firstLTSubPacketValue = ParsePacket(input, ref partOneSolution);
                    var secondLTSubPacketValue = ParsePacket(input, ref partOneSolution);
                    if (firstLTSubPacketValue < secondLTSubPacketValue) return 1;
                    else return 0;
                case PacketType.EqualTo:
                    var firstETSubPacketValue = ParsePacket(input, ref partOneSolution);
                    var secondETSubPacketValue = ParsePacket(input, ref partOneSolution);
                    if (firstETSubPacketValue == secondETSubPacketValue) return 1;
                    else return 0;
                default:
                    break;
            }
            return 0;
        }

        private long ReadLiteralValue(Queue<char> input)
        {
            var sb = new StringBuilder();
            var shouldContinueReading = true;
            do
            {
                shouldContinueReading = input.Dequeue() == '1';

                for (int i = 0; i < 4; i++)
                {
                    sb.Append(input.Dequeue());
                }
            }
            while (shouldContinueReading);

            return Convert.ToInt64(sb.ToString(), 2);
        }

        private long ReadDigits(Queue<char> input, int digitsToRead)
        {
            var digits = string.Empty;
            for (int i = 0; i < digitsToRead; i++)
            {
                digits += input.Dequeue();
            }
            return Convert.ToInt64(digits, 2);
        }
    }
}
