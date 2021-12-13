using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day13_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await ParseData("RealData.txt");
			var pointsAfterFold1 = Fold(data.Points, data.Folds.First());

			Console.WriteLine("How many dots are visible after completing just the first fold instruction on your transparent paper?");
			Console.WriteLine(pointsAfterFold1.Count);
		}

		public static ICollection<Point> Fold(IEnumerable<Point> points, Point foldLine)
		{
			Point FoldLeft(Point pp)
			{
				if (pp.X > foldLine.X)
				{
					var diff = pp.X - foldLine.X;
					return new Point(pp.X - (diff * 2), pp.Y);
				}

				return pp;
			}
			Point FoldUp(Point pp)
			{
				if (pp.Y > foldLine.Y)
				{
					var diff = pp.Y - foldLine.Y;
					return new Point(pp.X, pp.Y - (diff * 2));
				}

				return pp;
			}

			Func<Point, Point> foldOperation = foldLine.X > 0
									? FoldLeft
									: FoldUp;

			return points.Select(foldOperation)
						.Distinct()
						.ToList();
		}

		public static async Task<(IEnumerable<Point> Points, IEnumerable<Point> Folds)> ParseData(string file)
		{
			var lines = await File.ReadAllLinesAsync(file);

			var points = new List<Point>();
			var folds = new List<Point>();

			foreach (var line in lines.Where(l => ! string.IsNullOrEmpty(l)))
			{
				if (line.StartsWith("fold"))
				{
					var parts = line.Substring(11).Split('=');
					if (parts.Length != 2)
						throw new InvalidOperationException($"No equality sign in line: >{line}<");

					var foldLine = parts[0] switch
					{
						"x" => new Point(int.Parse(parts[1]), 0),
						"y" => new Point(0, int.Parse(parts[1])),
						_ => throw new InvalidOperationException($"Unknown fold line at: >{line}<")
					};

					folds.Add(foldLine);
				}
				else
				{
					var parts = line.Split(',');
					if (parts.Length != 2)
						throw new InvalidOperationException($"No comma in line: >{line}<");

					points.Add(new Point(int.Parse(parts[0]), int.Parse(parts[1])));
				}
			}

			return (points, folds);
		}
	}
}
