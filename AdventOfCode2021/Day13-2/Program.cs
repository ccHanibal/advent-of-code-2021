using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day13_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await Day13_1.Program.ParseData("RealData.txt");
			var points = data.Points;
			foreach (var fold in data.Folds)
			{
				points = Day13_1.Program.Fold(points, fold);
			}

			Console.WriteLine("What code do you use to activate the infrared thermal imaging camera system?");

			int maxX = points.Select(p => p.X).Max() + 1;
			int maxY = points.Select(p => p.Y).Max() + 1;

			char[][] field = new char[maxY][];
			for (int a = 0; a < maxY; a++)
			{
				field[a] = Enumerable.Repeat('.', maxX).ToArray();
			}

			points.ToList().ForEach(p => field[p.Y][p.X] = '#');

			// HKUJGAJZ
			for (int y = 0; y < maxY; y++)
			{
				Console.WriteLine(field[y]);
			}
		}
	}
}
