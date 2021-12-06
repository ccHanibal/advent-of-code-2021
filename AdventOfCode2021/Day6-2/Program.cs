using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Numerics;
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

			var sw = new Stopwatch();
			sw.Start();

			for (int a = 1; a <= 400000; a++)
			{
				try
				{
					var numNewFishes = fishes.Select(f => f.Replicate())
											.Where(f => f is not null)
											.Select(f => f.Value)
											.ToList();

					if (numNewFishes.Any())
						fishes.Add(new EquivalentFishes(8, Sum(numNewFishes)));

					if (fishes.Count > 1000)
					{
						fishes = GarbageCollection();
						//Console.WriteLine("Gen {0}: {1}", a, fishes.Count);
					}
				}
				catch (InvalidOperationException ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine("    at gen {0}", a);
					return;
				}
			}

			sw.Stop();

			Console.WriteLine("How many lanternfish would there be after 400000 days?");
			Console.WriteLine(Sum(fishes.Select(f => f.NumFishes)));
			Console.WriteLine();
			Console.WriteLine("Took {0}s", sw.ElapsedMilliseconds / 1000.0);

			List<EquivalentFishes> GarbageCollection()
			{
				return fishes.GroupBy(f => f.TimeTilReplication)
							.Select(g => new EquivalentFishes(g.Key, Sum(g.Select(f => f.NumFishes))))
							.ToList();
			}

			static BigInteger Sum(IEnumerable<BigInteger> source)
			{
				BigInteger result = 0;
				foreach (var num in source)
				{
					result += num;
				}

				return result;
			}
		}

		public class EquivalentFishes
		{
			public BigInteger NumFishes { get; private set; }
			public int TimeTilReplication { get; private set; }

			public EquivalentFishes(int timeTilReplication, BigInteger numFishes)
			{
				this.TimeTilReplication = timeTilReplication;
				this.NumFishes = numFishes;
			}

			public BigInteger? Replicate()
			{
				if (TimeTilReplication == 0)
				{
					TimeTilReplication = 6;
					return NumFishes;
				}

				TimeTilReplication--;
				return null;
			}
		}
	}
}
