using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			var fishesObj = (await File.ReadAllTextAsync("RealData.txt"))
							.Split(',')
							.Select(int.Parse)
							.GroupBy(t => t)
							.Select(g => new EquivalentFishes(g.Key, g.Count()))
							.ToList();
			var fishes = Enumerable.Range(0, 9)
									.Select(i => fishesObj.FirstOrDefault(f => f.TimeTilReplication == i)?.NumFishes ?? 0)
									.ToArray();

			var sw = new Stopwatch();
			sw.Start();

			int replicationZeroIndex = 0;

			for (int a = 1; a <= 400000; a++)
			{
				// Index + 7 is the new Time = 6 generation
				fishes[(replicationZeroIndex + 7) % 9] += fishes[replicationZeroIndex];
				replicationZeroIndex = (replicationZeroIndex + 1) % 9;

				// shifting the array instead
				//var tmp = fishes[0];
				//Array.Copy(fishes, 1, fishes, 0, fishes.Length - 1);
				//fishes[8] = tmp;
				//fishes[6] += tmp;
			}

			sw.Stop();

			Console.WriteLine("How many lanternfish would there be after 400000 days?");
			Console.WriteLine(Sum(fishes));
			Console.WriteLine();
			Console.WriteLine("Took {0}ms", sw.ElapsedMilliseconds);

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
