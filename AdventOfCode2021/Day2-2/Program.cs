using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day2_2
{
    public static class Program
    {
        private static readonly Regex cmdRegex = new Regex(@"(forward|up|down)\s(\d+)");

        public static async Task Main(string[] args)
        {
            var commands = await File.ReadAllLinesAsync("RealData.txt");

            int aim = 0;
            int depths = 0;
            int pos = 0;

            foreach (var cmd in commands)
            {
                var match = cmdRegex.Match(cmd);
                if (!match.Success)
                    throw new InvalidOperationException($"No match at >{cmd}<.");

                var cmdText = match.Groups[1].Value;
                if (!int.TryParse(match.Groups[2].Value, out int cmdValue))
                    throw new InvalidOperationException($"No integer value at >{cmd}<: {match.Groups[1].Value}.");

                switch (cmdText.ToLower())
                {
                    case "forward":
                        depths += aim * cmdValue;
                        pos += cmdValue;
                        break;

                    case "up":
                        aim -= cmdValue;
                        break;

                    case "down":
                        aim += cmdValue;
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown command at >{cmd}<: {cmdText}");
                }
            }

            Console.WriteLine("What do you get if you multiply your final horizontal position by your final depth?");
            Console.WriteLine(depths * pos);
        }
    }
}
