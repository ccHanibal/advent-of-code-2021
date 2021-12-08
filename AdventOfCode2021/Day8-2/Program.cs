using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day8_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var groups = (await File.ReadAllLinesAsync("RealData.txt"))
							.Select(l =>
							{
								var p = l.Split('|');
								return new
								{
									Segemnts = p[0].Trim()
												.Split(' ')
												.Select(s => s.AsOrderedString())
												.ToArray(),
									Output = p[1].Trim()
												.Split(' ')
												.Select(s => s.AsOrderedString())
												.ToArray()
								};
							})
							.ToList();

			int result = 0;

			foreach (var group in groups)
			{
				var segments = group.Segemnts;


				var num1 = segments.Single(s => s.Length == 2);
				var num4 = segments.Single(s => s.Length == 4);
				var num7 = segments.Single(s => s.Length == 3);
				var num8 = segments.Single(s => s.Length == 7);

				var aaaa = num7.Except(num1).Single();
				var cccc = segments.Count(p => p.Contains(num1[0])) == 8 ? num1[0] : num1[1];
				var ffff = num1.Single(p => p != cccc);

				var num4Unknonws = num4.Except(num1).ToArray();
				var bbbb = segments.Count(p => p.Contains(num4Unknonws[0])) == 6 ? num4Unknonws[0] : num4Unknonws[1];
				var dddd = num4Unknonws.Single(p => p != bbbb);

				var allKnownSoFar = new[] { aaaa, bbbb, cccc, dddd, ffff }.ToList();
				var gggg = segments.Where(s => s.Intersect(allKnownSoFar).Count() == allKnownSoFar.Count)
									.Select(s => s.Except(allKnownSoFar).ToArray())
									.Single(s => s.Length == 1)
									.First();

				allKnownSoFar.Add(gggg);

				var eeee = num8.Except(allKnownSoFar)
									.Single();

				var num2 = segments.Single(s => s.SequenceEqual(new[] { aaaa, cccc, dddd, eeee, gggg }.AsOrderedString()));
				var num3 = segments.Single(s => s.SequenceEqual(new[] { aaaa, cccc, dddd, ffff, gggg }.AsOrderedString()));
				var num5 = segments.Single(s => s.SequenceEqual(new[] { aaaa, bbbb, dddd, ffff, gggg }.AsOrderedString()));
				var num6 = segments.Single(s => s.SequenceEqual(new[] { aaaa, bbbb, dddd, eeee, ffff, gggg }.AsOrderedString()));
				var num9 = segments.Single(s => s.SequenceEqual(new[] { aaaa, bbbb, cccc, dddd, ffff, gggg }.AsOrderedString()));
				var num0 = segments.Single(s => s.SequenceEqual(new[] { aaaa, bbbb, cccc, eeee, ffff, gggg }.AsOrderedString()));

				var dict = new Dictionary<string, int>
				{
					{ num0, 0 },
					{ num1, 1 },
					{ num2, 2 },
					{ num3, 3 },
					{ num4, 4 },
					{ num5, 5 },
					{ num6, 6 },
					{ num7, 7 },
					{ num8, 8 },
					{ num9, 9 }
				};

				result += int.Parse(string.Concat(group.Output.Select(o => dict[o])));
				//Console.WriteLine("Result: {0}", result);
			}

			Console.WriteLine("What do you get if you add up all of the output values?");
			Console.WriteLine(result);
		}
	}

	public static class Ext
	{
		public static string AsOrderedString(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return str;

			return string.Concat(str.OrderBy(c => c));
		}
		public static string AsOrderedString(this IEnumerable<char> str)
		{
			if (str is null || !str.Any())
				return "";

			return string.Concat(str.OrderBy(c => c));
		}
	}
}
