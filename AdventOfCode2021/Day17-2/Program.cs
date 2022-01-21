using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Day17_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var targetArea = await Day17_1.Program.GetTargetAreaAsync("RealData.txt");

			Console.WriteLine("{0}, {1}", targetArea.Left, targetArea.Right);
			Console.WriteLine("{0}, {1}", targetArea.Top, targetArea.Bottom);
			Console.WriteLine(targetArea);
			Console.WriteLine();

			//var samplePoints = new[]
			//{
			//	new Point(23, -10), new Point(25, -9), new Point(27,  -5), new Point(29,  -6), new Point(22, -6), new Point(21,  -7), new Point( 9,   0), new Point(27,  -7), new Point(24, -5),
			//	new Point(25,  -7), new Point(26, -6), new Point(25,  -5), new Point( 6,   8), new Point(11, -2), new Point(20,  -5), new Point(29, -10), new Point( 6,   3), new Point(28, -7),
			//	new Point(8,    0), new Point(30, -6), new Point(29,  -8), new Point(20, -10), new Point( 6,  7), new Point( 6,   4), new Point( 6,   1), new Point(14,  -4), new Point(21, -6),
			//	new Point(26, -10), new Point( 7, -1), new Point( 7,   7), new Point( 8,  -1), new Point(21, -9), new Point( 6,   2), new Point(20,  -7), new Point(30, -10), new Point(14, -3),
			//	new Point(20,  -8), new Point(13, -2), new Point( 7,   3), new Point(28,  -8), new Point(29, -9), new Point(15,  -3), new Point(22,  -5), new Point(26,  -8), new Point(25, -8),
			//	new Point(25,  -6), new Point(15, -4), new Point( 9,  -2), new Point(15,  -2), new Point(12, -2), new Point(28,  -9), new Point(12,  -3), new Point(24,  -6), new Point(23, -7),
			//	new Point(25, -10), new Point( 7,  8), new Point(11,  -3), new Point(26,  -7), new Point( 7,  1), new Point(23,  -9), new Point( 6,   0), new Point(22, -10), new Point(27, -6),
			//	new Point( 8,   1), new Point(22, -8), new Point(13,  -4), new Point( 7,   6), new Point(28, -6), new Point(11,  -4), new Point(12,  -4), new Point(26,  -9), new Point( 7,  4),
			//	new Point(24, -10), new Point(23, -8), new Point(30,  -8), new Point( 7,   0), new Point( 9, -1), new Point(10,  -1), new Point(26,  -5), new Point(22,  -9), new Point( 6,  5),
			//	new Point( 7,   5), new Point(23, -6), new Point(28, -10), new Point(10,  -2), new Point(11, -1), new Point(20,  -9), new Point(14,  -2), new Point(29,  -7), new Point(13, -3),
			//	new Point(23,  -5), new Point(24, -8), new Point(27,  -9), new Point(30,  -7), new Point(28, -5), new Point(21, -10), new Point( 7,   9), new Point( 6,   6), new Point(21, -5),
			//	new Point(27, -10), new Point( 7,  2), new Point(30,  -9), new Point(21,  -8), new Point(22, -7), new Point(24,  -9), new Point(20,  -6), new Point( 6,   9), new Point(29, -5),
			//	new Point( 8,  -2), new Point(27, -8), new Point(30,  -5), new Point(24,  -7)
			//};

			//foreach (var samplePoint in samplePoints)
			//{
			//	if (!Day17_1.Program.CalcHighestPoint(samplePoint.X, samplePoint.Y, targetArea, out _))
			//	{
			//		Console.WriteLine("Init velocity {0}, {1} did not reach target area.", samplePoint.X, samplePoint.Y);
			//		Day17_1.Program.CalcHighestPoint(samplePoint.X, samplePoint.Y, targetArea, out _);
			//	}
			//}


			if (Day17_1.Program.CalcHighestPoint(25, -9, targetArea, out _))
				Console.WriteLine("Init velocity 25, -9 reached target area.");

			var initVelocitiesWithHit = new List<Point>();

			for (int x = 1; x <= targetArea.Right; x++)
			{
				for (int y = 1000; y >= targetArea.Top; y--)
				{
					if (Day17_1.Program.CalcHighestPoint(x, y, targetArea, out _))
					{
						initVelocitiesWithHit.Add(new Point(x, y));
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine("How many distinct initial velocity values cause the probe to be within the target area after any step?");
			Console.WriteLine(initVelocitiesWithHit.Count);
		}
	}
}
