using System;
using System.Linq;
using Day_18_1;
using static Day_18_1.SnailfishNumber;

namespace Day18_2
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var parser = new Parser("RealData.txt");
			var allNumbers = parser.Parse().ToList();

			if (allNumbers.Count <= 1)
				return;

			int maxMagnitude = 0;

			foreach (var sn1 in allNumbers)
			{
				foreach (var sn2 in allNumbers.Where(n => n != sn1))
				{
					var result = Add(sn1, sn2);
					Reduce(result);

					int magnitude = result.GetMagnitude();
					maxMagnitude = Math.Max(maxMagnitude, magnitude);
				}
			}

			Console.WriteLine(maxMagnitude);
		}
	}
}
