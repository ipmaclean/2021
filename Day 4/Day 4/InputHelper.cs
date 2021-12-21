using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_4
{
    public class InputHelper
    {
        public static (List<int>, List<BingoGrid>) Parse(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var callouts = new List<int>();
            var bingoGrids = new List<BingoGrid>();
            using (var sr = new StreamReader(input))
            {
                var numberRegex = new Regex(@"\d+");
                var ln = sr.ReadLine();
                var numberMatches = numberRegex.Matches(ln);
                for (int i = 0; i < numberMatches.Count; i++)
                {
                    callouts.Add(int.Parse(numberMatches[i].Value));
                }
                sr.ReadLine();

                while (ln != null)
                {
                    var bingoGrid = new BingoCell[5,5];
                    var rowIndex = 0;
                    while ((ln = sr.ReadLine()) != string.Empty && ln != null)
                    {
                        var bingoGridNumberMatches = numberRegex.Matches(ln);
                        for (int i = 0; i < bingoGridNumberMatches.Count; i++)
                        {
                            bingoGrid[rowIndex, i] = new BingoCell(int.Parse(bingoGridNumberMatches[i].Value));
                        }
                        rowIndex++;
                    }
                    bingoGrids.Add(new BingoGrid(bingoGrid));
                }
            }
            return (callouts, bingoGrids);
        }
    }
}
