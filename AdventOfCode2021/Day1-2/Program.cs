using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var numbers = (await File.ReadAllLinesAsync("RealData.txt"))
									.Select(l => int.Parse(l))
									.ToList();

			int numIncreases = 0;

			foreach (var pair in numbers.Pairwise3().Pairwise())
			{
				var sum2 = pair.Item.First + pair.Item.Second + pair.Item.Third;

				if (pair.Predecessor == null)
				{
					Console.WriteLine("{0} (N/A - no previous measurement)", sum2);
				}
				else
				{
					var sum1 = pair.Predecessor.Value.First + pair.Predecessor.Value.Second + pair.Predecessor.Value.Third;

					if (sum2 > sum1)
					{
						Console.WriteLine("{0} (increased)", sum2);
						numIncreases++;
					}
					else if (sum2 == sum1)
					{
						Console.WriteLine("{0} (no change)", sum2);
					}
					else
					{
						Console.WriteLine("{0} (decreased)", sum2);
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine("How many measurements are larger than the previous measurement?");
			Console.WriteLine(numIncreases);
		}
	}
}
