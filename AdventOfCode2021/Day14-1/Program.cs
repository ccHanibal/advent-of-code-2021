using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await ParseData("RealData.txt");
			var buffer = new StringBuilder(data.InitialValue, 1_000_000);

			for (int a = 0; a < 10; a++)
			{
				Console.WriteLine(a + 1);
				InsertionRound(buffer, data.Rules);
			}


			var histogram = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToDictionary(c => c, c => 0);
			for (int i = 0; i < buffer.Length; i++)
			{
				histogram[buffer[i]] += 1;
			}

			var ordered = histogram.Where(t => t.Value > 0)
									.OrderBy(t => t.Value)
									.ToList();
			var min = ordered[0];
			var max = ordered[^1];

			Console.WriteLine("What do you get if you take the quantity of the most common element and subtract the quantity of the least common element?");
			Console.WriteLine("{0} ({1}) - {2} ({3}) = {4}", max.Value, max.Key, min.Value, min.Key, max.Value - min.Value);
		}

		private static void InsertionRound(StringBuilder value, IEnumerable<InsertionRule> rules)
		{
			var insertions = rules.SelectMany(r => r.Apply(value))
									.OrderByDescending(t => t.Item1)
									.ToList();

			foreach (var insert in insertions)
			{
				value.Insert(insert.Item1, insert.Item2);
			}
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
			private readonly char insertChar;
			private readonly string findStr;

			public InsertionRule(string value, char insertChar)
			{
				this.insertChar = insertChar;
				this.findStr = value;
			}

			public IEnumerable<(int, char)> Apply(StringBuilder value)
			{
				return ApplyImpl().ToList();

				IEnumerable<(int, char)> ApplyImpl()
				{
					for (int a = 0; a < value.Length - 1; a++)
					{
						if (value[a] == findStr[0] && value[a + 1] == findStr[1])
							yield return (a + 1, insertChar);
					}
				}
			}
		}
	}
}
