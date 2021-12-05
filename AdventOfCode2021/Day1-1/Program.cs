using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Day1_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var numbers = (await File.ReadAllLinesAsync("RealData.txt"))
									.Select(l => int.Parse(l))
									.ToList();

			int numIncreases = 0;

			foreach (var pair in numbers.Pairwise())
			{
				if (pair.Predecessor == null)
				{
					Console.WriteLine("{0} (N/A - no previous measurement)", pair.Item);
				}
				else
				{
					if (pair.Item > pair.Predecessor)
					{
						Console.WriteLine("{0} (increased)", pair.Item);
						numIncreases++;
					}
					else
					{
						Console.WriteLine("{0} (decreased)", pair.Item);
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine("How many measurements are larger than the previous measurement?");
			Console.WriteLine(numIncreases);
		}
	}
}
