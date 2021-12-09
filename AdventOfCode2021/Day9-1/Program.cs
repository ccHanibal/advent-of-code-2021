using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day9_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var array = (await File.ReadAllLinesAsync("RealData.txt"))
							.Select(l => l.Select(c => c - 48).ToArray())
							.ToArray();

			int sum = 0;

			for (int col = 0; col < array.Length; col++)
			{
				for (int row = 0; row < array[col].Length; row++)
				{
					var pointVal = array[col][row];

					var adjPoints = new[]
					{
						new Point(row, col - 1), // up
						new Point(row - 1, col), // left
						new Point(row, col + 1), // down
						new Point(row + 1, col) // right
					};

					var isLowPoint = adjPoints
										.Where(p => p.X >= 0 && p.X < array[col].Length && p.Y >= 0 && p.Y < array.Length)
										.Select(p => array[p.Y][p.X])
										.All(v => v > pointVal);
					if (isLowPoint)
					{
						sum += (1 + pointVal);
					}
				}
			}

			Console.WriteLine("What is the sum of the risk levels of all low points on your heightmap?");
			Console.WriteLine(sum);
		}
	}
}
