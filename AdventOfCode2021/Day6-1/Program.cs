using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day6_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var fishes = (await File.ReadAllTextAsync("RealData.txt"))
							.Split(',')
							.Select(int.Parse)
							.ToList();

			for (int a = 1; a <= 80; a++)
			{
				int newFishes = 0;

				for (int fIdx = 0; fIdx < fishes.Count; fIdx++)
				{
					if (fishes[fIdx] == 0)
					{
						fishes[fIdx] = 6;
						newFishes++;
					}
					else
					{
						fishes[fIdx] = fishes[fIdx] - 1;
					}
				}

				fishes.AddRange(Enumerable.Repeat(8, newFishes));
			}

			Console.WriteLine("How many lanternfish would there be after 80 days?");
			Console.WriteLine(fishes.Count);
		}
	}
}
