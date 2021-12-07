using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day7_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var crabs = (await File.ReadAllTextAsync("RealData.txt"))
									.Split(',')
									.Select(int.Parse)
									.GroupBy(i => i)
									.Select(g => new Crabs { Height = g.Key, NumCrabs = g.Count() })
									.OrderBy(c => c.Height)
									.ToList();

			int fuel = 0;

			while (crabs.Any(h => h.Height != crabs[0].Height))
			{
				var firstGroup = crabs[0];
				var lastGroup = crabs[crabs.Count - 1];

				if (firstGroup.NumCrabs <= lastGroup.NumCrabs)
				{
					firstGroup.Height++;
					fuel += firstGroup.NumCrabs;

					var nextGroup = crabs[1];

					if (firstGroup.Height == nextGroup.Height)
					{
						// merge groups of same height
						nextGroup.NumCrabs += firstGroup.NumCrabs;
						crabs.RemoveAt(0);
					}
				}
				else
				{
					lastGroup.Height--;
					fuel += lastGroup.NumCrabs;

					var secondLastGroup = crabs[^2];

					if (lastGroup.Height == secondLastGroup.Height)
					{
						// merge groups of same height
						secondLastGroup.NumCrabs += lastGroup.NumCrabs;
						crabs.RemoveAt(crabs.Count - 1);
					}
				}
			}

			Console.WriteLine("How much fuel must they spend to align to that position?");
			Console.WriteLine("Positon: {0}", crabs[0].Height);
			Console.WriteLine("Fuel: {0}", fuel);
		}

		private class Crabs
		{
			public int NumCrabs { get; set; }
			public int Height { get; set; }

			public override string ToString()
			{
				return $"Height: {Height}, Num: {NumCrabs}";
			}
		}
	}
}
