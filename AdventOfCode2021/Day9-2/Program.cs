using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day9_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var array = (await File.ReadAllLinesAsync("RealData.txt"))
							.Select(l => l.Select(c => (bool?)(c == '9')).ToArray())
							.ToArray();

			var threeLargestGroups =
					Enumerable.Range(0, int.MaxValue)
								.Select(i => GetNextGroupSizeAndMark(array))
								.TakeWhile(s => s > 0)
								.OrderByDescending(x => x)
								.Take(3)
								.ToArray();

			Console.WriteLine("What do you get if you multiply together the sizes of the three largest basins?");
			Console.WriteLine(threeLargestGroups.Aggregate((a, b) => a * b));
		}

		private static int GetNextGroupSizeAndMark(bool?[][] array)
		{
			var groupStart = GetNextGroupStart();
			if (groupStart is null)
				return 0;

			var knownPoints = new SortedSet<Point>(new PointComparer())
			{
				groupStart.Value
			};

			var pointsToLookAt = new Queue<Point>();
			pointsToLookAt.Enqueue(groupStart.Value);

			while (pointsToLookAt.TryDequeue(out var curPoint))
			{
				MarkPointDone(curPoint);

				foreach (var p in GetNeighboursInGroup(curPoint))
				{
					if (knownPoints.Add(p))
						pointsToLookAt.Enqueue(p); // look only at new points
				}
			}

			return knownPoints.Count;

			Point[] GetNeighboursInGroup(Point pp)
			{
				var adjPoints = new[]
				{
					new Point(pp.X, pp.Y - 1), // up
					new Point(pp.X - 1, pp.Y), // left
					new Point(pp.X, pp.Y + 1), // down
					new Point(pp.X + 1, pp.Y) // right
				};

				var nonWallPoints = adjPoints
										.Where(p => p.X >= 0 && p.X < array[pp.Y].Length && p.Y >= 0 && p.Y < array.Length)
										.Where(p => array[p.Y][p.X] is not null && !array[p.Y][p.X].Value)
										.ToArray();

				return nonWallPoints;
			}

			Point? GetNextGroupStart()
			{
				for (int col = 0; col < array.Length; col++)
				{
					for (int row = 0; row < array[col].Length; row++)
					{
						var pointVal = array[col][row];
						if (pointVal is null || pointVal.Value)
							continue;

						return new Point(row, col);
					}
				}

				return null;
			}

			void MarkPointDone(Point p)
			{
				array[p.Y][p.X] = null;
			}
		}
	}

	public class PointComparer : IComparer<Point>
	{
		public int Compare(Point x, Point y)
		{
			var compX = x.X.CompareTo(y.X);
			if (compX != 0)
				return compX;

			return x.Y.CompareTo(y.Y);
		}
	}
}
