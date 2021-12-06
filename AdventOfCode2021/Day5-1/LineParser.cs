using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day5_1
{
	public class LineParser
	{
		private static readonly Regex segmentRegex = new Regex(@"^(\d+),(\d+)\s+->\s+(\d+),(\d+)$");

		private readonly string inputFile;

		public LineParser(string inputFile)
		{
			this.inputFile = inputFile;
		}

		public async Task<IEnumerable<Point>> ParseAsync(bool allowDiagonls)
		{
			var lines = await File.ReadAllLinesAsync(inputFile);
			return ParseImpl();

			IEnumerable<Point> ParseImpl()
			{
				foreach (var line in lines)
				{
					var matches = segmentRegex.Match(line);
					if (!matches.Success)
						throw new InvalidOperationException($"No match at >{line}<.");

					var p1 = CreatePoint(1, 2);
					var p2 = CreatePoint(3, 4);

					if (!allowDiagonls && p1.X != p2.X && p1.Y != p2.Y)
						continue;

					var xInc = GetInc(p => p.X);
					var yInc = GetInc(p => p.Y);

					var p = p1;

					for (; p.X != p2.X || p.Y != p2.Y; p.Offset(xInc, yInc))
					{
						yield return p;
					}

					yield return p2;

					Point CreatePoint(int xIdx, int yIdx)
					{
						return new Point(int.Parse(matches.Groups[xIdx].Value), int.Parse(matches.Groups[yIdx].Value));
					}
					int GetInc(Func<Point, int> coordSelector)
					{
						var diff = coordSelector(p2) - coordSelector(p1);
						if (diff == 0)
							return 0;

						return diff < 0 ? -1 : 1;
					}
				}
			}
		}
	}
}
