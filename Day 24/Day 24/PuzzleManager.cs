using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_24
{
    class PuzzleManager
    {
        public string InputFileName { get; set; }
        public List<Instruction> Instructions { get; set; }

        public PuzzleManager(string inputFileName)
        {
            InputFileName = inputFileName;
            Instructions = InputHelper.Parse(inputFileName);
        }

        public void Solve(long inputNumber, string puzzlePart)
        {
            var foundValidModelNumber = false;
            while (!foundValidModelNumber)
            {
                var variables = new Dictionary<string, long>()
                {
                    { "w" , 0 },
                    { "x" , 0 },
                    { "y" , 0 },
                    { "z" , 0 }
                };
                var currentInputIndex = 0;
                foreach (var instruction in Instructions)
                {
                    if (instruction.Name == "inp")
                    {

                    }
                    switch (instruction.Name)
                    {
                        case "inp":
                            long currentInput = inputNumber.ToString()[currentInputIndex] - '0';
                            Input(instruction.Value1, currentInput, variables);
                            currentInputIndex++;
                            break;
                        case "add":
                            Add(instruction.Value1, instruction.Value2, variables);
                            break;
                        case "mul":
                            Multiply(instruction.Value1, instruction.Value2, variables);
                            break;
                        case "div":
                            Divide(instruction.Value1, instruction.Value2, variables);
                            break;
                        case "mod":
                            Modulo(instruction.Value1, instruction.Value2, variables);
                            break;
                        case "eql":
                            Equals(instruction.Value1, instruction.Value2, variables);
                            break;
                        default:
                            throw new ArgumentException("Bad input");

                    }
                }
                if (variables["z"] == 0)
                {
                    foundValidModelNumber = true;
                    break;
                }
                inputNumber = DecrementModelNumber(inputNumber);
            }
            Console.WriteLine($"The solution to part {puzzlePart} is {inputNumber}.");
        }

        private long DecrementModelNumber(long inputNumber)
        {
            inputNumber--;
            var numberChanged = true;
            while (numberChanged)
            {
                numberChanged = false;
                for (int i = 0; i < inputNumber.ToString().Length; i++)
                {
                    if (inputNumber.ToString()[13 - i] == '0')
                    {
                        inputNumber -= (long)Math.Pow(10, i);
                        numberChanged = true;
                        break;
                    }
                }
            }
            return inputNumber;
        }

        private void Input(string value1, long input, Dictionary<string, long> variables)
        {
            variables[value1] = input;
        }

        private void Add(string value1, string value2, Dictionary<string, long> variables)
        {
            var firstValue = variables[value1];
            if (!long.TryParse(value2, out var secondValue))
            {
                secondValue = variables[value2];
            }
            variables[value1] = firstValue + secondValue;
        }

        private void Multiply(string value1, string value2, Dictionary<string, long> variables)
        {
            var firstValue = variables[value1];
            if (!long.TryParse(value2, out var secondValue))
            {
                secondValue = variables[value2];
            }
            variables[value1] = firstValue * secondValue;
        }

        private void Divide(string value1, string value2, Dictionary<string, long> variables)
        {
            var firstValue = variables[value1];
            if (!long.TryParse(value2, out var secondValue))
            {
                secondValue = variables[value2];
            }
            variables[value1] = firstValue / secondValue;
        }

        private void Modulo(string value1, string value2, Dictionary<string, long> variables)
        {
            var firstValue = variables[value1];
            if (!long.TryParse(value2, out var secondValue))
            {
                secondValue = variables[value2];
            }
            variables[value1] = firstValue % secondValue;
        }

        private void Equals(string value1, string value2, Dictionary<string, long> variables)
        {
            var firstValue = variables[value1];
            if (!long.TryParse(value2, out var secondValue))
            {
                secondValue = variables[value2];
            }

            if (firstValue == secondValue)
            {
                variables[value1] = 1;
            }
            else
            {
                variables[value1] = 0;
            }

        }
    }
}
