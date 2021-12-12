using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day11_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var octopussi = (await File.ReadAllLinesAsync("RealData.txt"))
										.Select(x => x.Select(c => new Octopus(c - 48)).ToArray())
										.ToArray();
			var allOctopussi = octopussi.SelectMany(o => o)
										.ToList();

			int numFlashes = 0;

			for (int a = 0; a < 100; a++)
			{
                //Console.WriteLine("Before Step {0}", a + 1);

                //for (int row = 0; row < octopussi.Length; row++)
                //{
                //    for (int col = 0; col < octopussi[row].Length; col++)
                //    {
                //        Console.Write(octopussi[row][col].EnergyLevel);
                //    }

                //    Console.WriteLine();
                //}

                allOctopussi.ForEach(o => o.IncreaseEnergy());

				var processed = new HashSet<Octopus>();

				while (true)
				{
					bool hasFlashingOcurred = false;

					for (int row = 0; row < octopussi.Length; row++)
					{
						for (int col = 0; col < octopussi[row].Length; col++)
						{
							if (octopussi[row][col].IsFlashing && processed.Add(octopussi[row][col]))
							{
								hasFlashingOcurred = true;
								numFlashes++;

								var adjPoints = new[]
								{
									new Point(row - 1, col - 1),
									new Point(row, col - 1),
									new Point(row + 1, col - 1),
									new Point(row - 1, col),
									new Point(row + 1, col),
									new Point(row + 1, col + 1),
									new Point(row, col + 1),
									new Point(row - 1, col + 1)
								};

								var validNeighbors = adjPoints
														.Where(p => p.X >= 0 && p.X < octopussi[row].Length && p.Y >= 0 && p.Y < octopussi.Length)
														.ToList();

								validNeighbors.ForEach(p => octopussi[p.X][p.Y].IncreaseEnergy());
							}
						}
					}

					if (!hasFlashingOcurred)
						break;
				}

				allOctopussi.ForEach(o => o.Reset());
			}

			Console.WriteLine("How many total flashes are there after 100 steps?");
			Console.WriteLine(numFlashes);
		}
	}

    public class Octopus
    {
        public int EnergyLevel { get; private set; }
		public bool IsFlashing { get; private set; }

		public Octopus(int energyLevel)
		{
			this.EnergyLevel = energyLevel;
		}

		public void IncreaseEnergy()
		{
			if (EnergyLevel > 9)
				return;

			EnergyLevel++;

			if (EnergyLevel > 9)
				IsFlashing = true;
		}

		public void Reset()
		{
			if (IsFlashing)
            {
				IsFlashing = false;
				EnergyLevel = 0;
            }
		}
    }
}
