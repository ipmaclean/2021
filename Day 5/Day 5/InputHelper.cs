using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_5
{
    public class InputHelper
    {
        public static Dictionary<(int, int), int> ParsePartOne(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var vents = new Dictionary<(int, int), int>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    var numberRegex = new Regex(@"\d+");
                    var numberMatches = numberRegex.Matches(ln);

                    var oldXValue = int.Parse(numberMatches[0].Value);
                    var oldYValue = int.Parse(numberMatches[1].Value);
                    var newXValue = int.Parse(numberMatches[2].Value);
                    var newYValue = int.Parse(numberMatches[3].Value);

                    if (oldXValue == newXValue)
                    {
                        for (int i = Math.Min(oldYValue, newYValue); i <= Math.Max(oldYValue, newYValue); i++)
                        {
                            if (vents.ContainsKey((oldXValue, i)))
                            {
                                vents[(oldXValue, i)]++;
                            }
                            else
                            {
                                vents.Add((oldXValue, i), 1);
                            }
                        }
                    }
                    else if(oldYValue == newYValue)
                    {
                        for (int i = Math.Min(oldXValue, newXValue); i <= Math.Max(oldXValue, newXValue); i++)
                        {
                            if (vents.ContainsKey((i, oldYValue)))
                            {
                                vents[(i, oldYValue)]++;
                            }
                            else
                            {
                                vents.Add((i, oldYValue), 1);
                            }
                        }
                    }
                }
            }
            return vents;
        }

        public static Dictionary<(int, int), int> ParsePartTwo(string fileName)
        {
            var input = Path.Combine("..", "..", "..", fileName);
            var vents = new Dictionary<(int, int), int>();
            using (var sr = new StreamReader(input))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {
                    var numberRegex = new Regex(@"\d+");
                    var numberMatches = numberRegex.Matches(ln);

                    var oldXValue = int.Parse(numberMatches[0].Value);
                    var oldYValue = int.Parse(numberMatches[1].Value);
                    var newXValue = int.Parse(numberMatches[2].Value);
                    var newYValue = int.Parse(numberMatches[3].Value);

                    if (oldXValue == newXValue)
                    {
                        for (int i = Math.Min(oldYValue, newYValue); i <= Math.Max(oldYValue, newYValue); i++)
                        {
                            if (vents.ContainsKey((oldXValue, i)))
                            {
                                vents[(oldXValue, i)]++;
                            }
                            else
                            {
                                vents.Add((oldXValue, i), 1);
                            }
                        }
                    }
                    else if (oldYValue == newYValue)
                    {
                        for (int i = Math.Min(oldXValue, newXValue); i <= Math.Max(oldXValue, newXValue); i++)
                        {
                            if (vents.ContainsKey((i, oldYValue)))
                            {
                                vents[(i, oldYValue)]++;
                            }
                            else
                            {
                                vents.Add((i, oldYValue), 1);
                            }
                        }
                    }
                    else
                    {
                        if (oldXValue < newXValue && oldYValue < newYValue)
                        {
                            for (int i = 0; i <= Math.Abs(oldXValue - newXValue); i++)
                            {
                                if (vents.ContainsKey((oldXValue + i, oldYValue + i)))
                                {
                                    vents[(oldXValue + i, oldYValue + i)]++;
                                }
                                else
                                {
                                    vents.Add((oldXValue + i, oldYValue + i), 1);
                                }
                            }
                        }
                        else if (oldXValue < newXValue && oldYValue > newYValue)
                        {
                            for (int i = 0; i <= Math.Abs(oldXValue - newXValue); i++)
                            {
                                if (vents.ContainsKey((oldXValue + i, oldYValue - i)))
                                {
                                    vents[(oldXValue + i, oldYValue - i)]++;
                                }
                                else
                                {
                                    vents.Add((oldXValue + i, oldYValue - i), 1);
                                }
                            }
                        }
                        else if (oldXValue > newXValue && oldYValue > newYValue)
                        {
                            for (int i = 0; i <= Math.Abs(oldXValue - newXValue); i++)
                            {
                                if (vents.ContainsKey((oldXValue - i, oldYValue - i)))
                                {
                                    vents[(oldXValue - i, oldYValue - i)]++;
                                }
                                else
                                {
                                    vents.Add((oldXValue - i, oldYValue - i), 1);
                                }
                            }
                        }
                        else if (oldXValue > newXValue && oldYValue < newYValue)
                        {
                            for (int i = 0; i <= Math.Abs(oldXValue - newXValue); i++)
                            {
                                if (vents.ContainsKey((oldXValue - i, oldYValue + i)))
                                {
                                    vents[(oldXValue - i, oldYValue + i)]++;
                                }
                                else
                                {
                                    vents.Add((oldXValue - i, oldYValue + i), 1);
                                }
                            }
                        }
                    }
                }
            }
            return vents;
        }
    }
}
