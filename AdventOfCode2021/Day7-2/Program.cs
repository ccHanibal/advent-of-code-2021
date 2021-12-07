using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day7_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var allCrabs = (await File.ReadAllTextAsync("RealData.txt"))
										.Split(',')
										.Select(p => new Crab(int.Parse(p)))
										.OrderBy(c => c.Height)
										.ToList();

			int fuel = 0;

			while (allCrabs.Any(h => h.Height != allCrabs[0].Height))
			{
				var groups = allCrabs.GroupBy(c => c.Height)
								.Select(g => new { Height = g.Key, Crabs = g.ToList() })
								.OrderBy(c => c.Height)
								.ToList();

				var firstGroup = groups[0];
				var lastGroup = groups[^1];
				var fuelFirstGroup = CalcFuelOneMove(firstGroup.Crabs);
				var fuelLastGroup = CalcFuelOneMove(lastGroup.Crabs);

				if (fuelFirstGroup <= fuelLastGroup)
				{
					firstGroup.Crabs.ForEach(c => c.Move(true));
					fuel += fuelFirstGroup;
				}
				else
				{
					lastGroup.Crabs.ForEach(c => c.Move(false));
					fuel += fuelLastGroup;
				}

				static int CalcFuelOneMove(IEnumerable<Crab> crabs)
				{
					return crabs.Select(c => c.Moves + 1).Sum();
				}
			}

			Console.WriteLine("How much fuel must they spend to align to that position?");
			Console.WriteLine("Positon: {0}", allCrabs[0].Height);
			Console.WriteLine("Fuel: {0}", fuel);
		}

		private class Crab
		{
			public int Moves { get; private set; } = 0;
			public int Height { get; private set; }

			public Crab(int height)
			{
				this.Height = height;
			}

			public void Move(bool up)
			{
				if (up)
					Height++;
				else
					Height--;

				Moves++;
			}
		}
	}
}
