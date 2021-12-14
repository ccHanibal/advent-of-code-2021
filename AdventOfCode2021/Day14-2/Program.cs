using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var (initVal, rules) = await ParseData("RealData.txt");

			var histogram = CreateHistogram(initVal);

			for (int a = 0; a < 40; a++)
			{
				Console.WriteLine(a + 1);
				histogram = InsertionRound(histogram, rules);
			}

			var ordered = histogram.Where(t => t.Value > 0)
									.SelectMany(t => new[]
									{
										(t.Key[0], t.Value),
										(t.Key[1], t.Value)
									})
									.GroupBy(t => t.Item1)
									.Select(g =>
									{
										var sum = g.Select(t => t.Value).Sum();

										if (g.Key == initVal[0] || g.Key == initVal[^1])
										{
											// first and last char double counted one less
											return (g.Key, Value: ((sum - 1) / 2) + 1);
										}

										// chars are double counted
										return (g.Key, Value: sum / 2);
									})
									.OrderBy(t => t.Value)
									.ToList();
			var min = ordered[0];
			var max = ordered[^1];

			Console.WriteLine("What do you get if you take the quantity of the most common element and subtract the quantity of the least common element?");
			Console.WriteLine("{0} ({1}) - {2} ({3}) = {4}", max.Value, max.Key, min.Value, min.Key, max.Value - min.Value);
		}

		private static IDictionary<string, long> CreateHistogram(string initialValue)
		{
			var histogram = new Dictionary<string, long>();

			for (int a = 1; a < initialValue.Length; a++)
			{
				var pair = initialValue.Substring(a - 1, 2);
				if (histogram.ContainsKey(pair))
					histogram[pair]++;
				else
					histogram.Add(pair, 1);
			}

			return histogram;
		}

		private static IDictionary<string, long> InsertionRound(IDictionary<string, long> histogram, IEnumerable<InsertionRule> rules)
		{
			var newHist = new Dictionary<string, long>();

			foreach (var r in rules)
			{
				if (histogram.TryGetValue(r.FindStr, out long num) && num > 0)
				{
					foreach (var pair in r.NextPairs)
					{
						if (newHist.ContainsKey(pair))
							newHist[pair] += num;
						else
							newHist.Add(pair, num);
					}
				}
			}

			return newHist;
		}

		public static async Task<(string InitialValue, IEnumerable<InsertionRule> Rules)> ParseData(string file)
		{
			var lines = await File.ReadAllLinesAsync(file);

			var initVal = "";
			var rules = new List<InsertionRule>();

			foreach (var line in lines.Where(l => !string.IsNullOrEmpty(l)))
			{
				var indexArrow = line.IndexOf(" -> ");
				if (indexArrow >= 0)
				{
					rules.Add(new InsertionRule(line.Substring(0, indexArrow), line[indexArrow + 4]));
				}
				else
				{
					initVal = line;
				}
			}

			return (initVal, rules);
		}

		public class InsertionRule
		{
			public string FindStr { get; }
			public string[] NextPairs { get; }

			public InsertionRule(string value, char insertChar)
			{
				this.FindStr = value;

				this.NextPairs = new[]
				{
					string.Concat(FindStr[0], insertChar),
					string.Concat(insertChar, FindStr[1]),
				};
			}
		}
	}
}
