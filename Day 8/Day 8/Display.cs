using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8
{
    public class Display
    {
        public string[] Digits { get; set; }
        public string[] Output { get; set; }

        public Display(string[] digits, string[] output)
        {
            Digits = digits;
            Output = output;
        }

        public int CountPartOneDigits()
            =>  Output.Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);

        public int CalculateOutput(Dictionary<string, string> displayToNumeralDict)
        {
            var decryptDictionary = new Dictionary<char, char>();
            DecryptDigits(decryptDictionary);
            return ParseOutput(decryptDictionary, displayToNumeralDict);
        }

        private void DecryptDigits(Dictionary<char, char> decryptDictionary)
        {
            FindASegment(decryptDictionary);
            FindSegmentsFromCountInstances(decryptDictionary);
            FindDSegment(decryptDictionary);
            FindGSegment(decryptDictionary);
        }

        private void FindASegment(Dictionary<char, char> decryptDictionary)
        {
            var lengthTwoDigit = Digits.First(d => d.Length == 2);
            var lengthThreeDigit = Digits.First(d => d.Length == 3);

            foreach (var letter in lengthTwoDigit)
            {
                lengthThreeDigit = lengthThreeDigit.Replace(letter.ToString(), "");
            }

            decryptDictionary.Add(char.Parse(lengthThreeDigit), 'a');
        }

        private void FindSegmentsFromCountInstances(Dictionary<char, char> decryptDictionary)
        {
            var countInstances = new Dictionary<char, int>()
            {
                { 'a', 0 },
                { 'b', 0 },
                { 'c', 0 },
                { 'd', 0 },
                { 'e', 0 },
                { 'f', 0 },
                { 'g', 0 }
            };

            foreach (var digit in Digits)
            {
                foreach (var key in countInstances.Keys)
                {
                    if (digit.Contains(key))
                    {
                        countInstances[key]++;
                    }
                }
            }

            foreach (var countInstance in countInstances)
            {
                if (countInstance.Value == 8 && !decryptDictionary.Keys.Contains(countInstance.Key))
                {
                    decryptDictionary.Add(countInstance.Key, 'c');
                }
                if (countInstance.Value == 6)
                {
                    decryptDictionary.Add(countInstance.Key, 'b');
                }
                if (countInstance.Value == 4)
                {
                    decryptDictionary.Add(countInstance.Key, 'e');
                }
                if (countInstance.Value == 9)
                {
                    decryptDictionary.Add(countInstance.Key, 'f');
                }
            }
        }

        private void FindDSegment(Dictionary<char, char> decryptDictionary)
        {
            var lengthFourDigit = Digits.First(d => d.Length == 4);
            foreach (var decryption in decryptDictionary)
            {
                if (lengthFourDigit.Contains(decryption.Key))
                {
                    lengthFourDigit = lengthFourDigit.Replace(decryption.Key.ToString(), "");
                }
            }
            decryptDictionary.Add(char.Parse(lengthFourDigit), 'd');
        }

        private void FindGSegment(Dictionary<char, char> decryptDictionary)
        {
            var lengthSevenDigit = Digits.First(d => d.Length == 7);
            foreach (var decryption in decryptDictionary)
            {
                if (lengthSevenDigit.Contains(decryption.Key))
                {
                    lengthSevenDigit = lengthSevenDigit.Replace(decryption.Key.ToString(), "");
                }
            }
            decryptDictionary.Add(char.Parse(lengthSevenDigit), 'g');
        }

        private int ParseOutput(Dictionary<char, char> decryptDictionary, Dictionary<string, string> displayToNumeralDict)
        {
            var output = string.Empty;

            foreach(var digit in Output)
            {
                var trueDigit = string.Empty;
                foreach (var wire in digit)
                {
                    trueDigit += decryptDictionary[wire];
                }
                output += displayToNumeralDict[SortString(trueDigit)];
            }

            return int.Parse(output);
        }

        string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}
