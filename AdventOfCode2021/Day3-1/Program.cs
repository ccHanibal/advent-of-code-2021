using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Day3_1
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var diagNumbers = await File.ReadAllLinesAsync("RealData.txt");

                string gammaRateText = Enumerable
                                            .Range(0, diagNumbers[0].Length)
                                            .Select(i => diagNumbers
                                                            .GroupBy(n => n[i])
                                                            .Select(g => new { Bit = g.Key, Count = g.Count() })
                                                            .MaxBy(t => t.Count)
                                                            .Select(t => t.Bit)
                                                            .First())
                                            .ToDelimitedString("");

                string epsilonText = gammaRateText
                                        .Select(c => c == '0' ? '1' : '0')
                                        .ToDelimitedString("");

                int gammaRate = Convert.ToInt32(gammaRateText, 2);
                int epsilon = Convert.ToInt32(epsilonText, 2);

                Console.WriteLine("What is the power consumption of the submarine?");
                Console.WriteLine(gammaRate * epsilon);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
