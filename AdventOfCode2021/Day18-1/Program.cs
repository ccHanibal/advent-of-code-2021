using System;
using System.Linq;
using static Day_18_1.SnailfishNumber;

namespace Day_18_1
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var parser = new Parser("RealData.txt");
			var allNumbers = parser.Parse().ToList();

			if (allNumbers.Count <= 1)
				return;

			var result = Add(allNumbers[0], allNumbers[1]);
			Reduce(result);

			for (int a = 2; a < allNumbers.Count; a++)
			{
				result = Add(result, allNumbers[a]);
				Reduce(result);
			}

			Console.WriteLine(result);
			Console.WriteLine("Magnitude: {0}", result.GetMagnitude());
		}
	}
}
