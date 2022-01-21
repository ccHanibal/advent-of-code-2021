using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day17_1
{
	public static class Program
	{
		private static readonly Regex targetAreaRegex = new Regex(@"target\s+area:\s+x=(?<x1>-?\d+)\.\.(?<x2>-?\d+),\s+y=(?<y1>-?\d+)\.\.(?<y2>-?\d+)");

		public static async Task Main(string[] args)
		{
			var targetArea = await GetTargetAreaAsync("RealData.txt");

			Console.WriteLine("{0}, {1}", targetArea.Left, targetArea.Right);
			Console.WriteLine("{0}, {1}", targetArea.Top, targetArea.Bottom);
			Console.WriteLine(targetArea);
			Console.WriteLine();

			// Samples
			//if (CalcHighestPoint(7, 2, targetArea, out _))
			//	Console.WriteLine("Init velocity 7, 2 reached target area.");

			//if (CalcHighestPoint(6, 3, targetArea, out _))
			//	Console.WriteLine("Init velocity 6, 3 reached target area.");

			//if (CalcHighestPoint(9, 0, targetArea, out _))
			//	Console.WriteLine("Init velocity 9, 0 reached target area.");

			//if (!CalcHighestPoint(17, -4, targetArea, out _))
			//	Console.WriteLine("Init velocity 17, -4 did not reach target area.");

			//if (CalcHighestPoint(6, 9, targetArea, out _))
			//	Console.WriteLine("Init velocity 6, 9 reached target area.");

			//if (CalcHighestPoint(6, 10, targetArea, out _))
			//	Console.WriteLine("Init velocity 6, 10 reached target area.");

			Point? initVelocityWithHighestPoint = null;
			Point highestPoint = new Point(0, int.MinValue);

			for (int x = 1; x < targetArea.Right; x++)
			{
				for (int y = 1000; y > targetArea.Top ; y--)
				{
					if (CalcHighestPoint(x, y, targetArea, out var highPoint) && highestPoint.Y < highPoint.Y)
					{
						initVelocityWithHighestPoint = new Point(x, y);
						highestPoint = highPoint;
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine("What is the initial velocity with the highest point on its trajectory?");
			Console.WriteLine(initVelocityWithHighestPoint);

			Console.WriteLine();
			Console.WriteLine("What is the highest y position it reaches on this trajectory?");
			Console.WriteLine(highestPoint);
		}

		public static bool CalcHighestPoint(int velocityX, int velocityY, Rectangle targetArea, out Point highest)
		{
			int posX = 0;
			int posY = 0;

			highest = new Point(0, 0);

			targetArea = Rectangle.FromLTRB(
							targetArea.Left,
							targetArea.Top,
							targetArea.Right + 1,
							targetArea.Bottom + 1);

			while (posX <= targetArea.Right && posY >= targetArea.Top)
			{
				posX += velocityX;
				posY += velocityY;

				if (velocityX > 0)
					velocityX--;

				velocityY--;

				if (velocityY == 0)
					highest = new Point(posX, posY);

				if (targetArea.Contains(posX, posY))
					return true;
			}

			return false;
		}

		public static async Task<Rectangle> GetTargetAreaAsync(string filename)
		{
			var data = await File.ReadAllTextAsync(filename);
			var targetMatch = targetAreaRegex.Match(data);
			if (targetMatch.Success)
			{
				int x1 = int.Parse(targetMatch.Groups["x1"].Value);
				int x2 = int.Parse(targetMatch.Groups["x2"].Value);
				int y1 = int.Parse(targetMatch.Groups["y1"].Value);
				int y2 = int.Parse(targetMatch.Groups["y2"].Value);

				return Rectangle.FromLTRB(
							Math.Min(x1, x2),
							Math.Min(y1, y2),
							Math.Max(x1, x2),
							Math.Max(y1, y2));
			}

			throw new InvalidOperationException("Uknown target area format");
		}
	}
}
