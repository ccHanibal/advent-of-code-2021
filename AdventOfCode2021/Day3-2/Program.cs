using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day3_2
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var diagNumbers = await File.ReadAllLinesAsync("RealData.txt");

                string oxygenGenRatingText = Enumerable
                                                .Range(0, diagNumbers[0].Length)
                                                .Aggregate(
                                                    diagNumbers.ToList(),
                                                    (numsLeft, i) => numsLeft.WithCommonBit(i, CommonBit.Most).ToList(),
                                                    nums => nums.First());

                string co2ScrubRatingText = Enumerable
                                                .Range(0, diagNumbers[0].Length)
                                                .Aggregate(
                                                    diagNumbers.ToList(),
                                                    (numsLeft, i) => numsLeft.WithCommonBit(i, CommonBit.Least).ToList(),
                                                    nums => nums.First());

                int oxygenGenRating = Convert.ToInt32(oxygenGenRatingText, 2);
                int co2ScrubRating = Convert.ToInt32(co2ScrubRatingText, 2);

                Console.WriteLine("What is the life support rating of the submarine?");
                Console.WriteLine(oxygenGenRating * co2ScrubRating);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
