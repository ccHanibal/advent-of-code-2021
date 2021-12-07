using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day6_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var fishes = (await File.ReadAllTextAsync("RealData.txt"))
							.Split(',')
							.Select(int.Parse)
							.GroupBy(t => t)
							.Select(g => new EquivalentFishes(g.Key, g.Count()))
							.ToList();

			// you can find a multiple magnitude faster version in the feature/400000-gens-day6 branch

			for (int a = 1; a <= 256; a++)
			{
				var newFishes = fishes.Select(f => f.Replicate())
									.Where(f => f is not null)
									.ToList();

				if (newFishes.Any())
					fishes.Add(new EquivalentFishes(8, newFishes.Select(f => f.NumFishes).Sum()));
			}

			Console.WriteLine("How many lanternfish would there be after 256 days?");
			Console.WriteLine(fishes.Select(f => f.NumFishes).Sum());
		}

		public class EquivalentFishes
		{
			public long NumFishes { get; private set; }
			public int TimeTilReplication { get; private set; }

			public EquivalentFishes(int timeTilReplication, long numFishes)
			{
				this.TimeTilReplication = timeTilReplication;
				this.NumFishes = numFishes;
			}

			public EquivalentFishes Replicate()
			{
				if (TimeTilReplication == 0)
				{
					TimeTilReplication = 6;
					return new EquivalentFishes(8, NumFishes);
				}

				TimeTilReplication--;
				return null;
			}
		}
	}
}
