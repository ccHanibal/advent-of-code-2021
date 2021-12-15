using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

namespace Day15_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			static char CalcNext(char c, int offset)
			{
				var cur = c - 48;

				for (int a = 0; a < offset; a++)
				{
					cur++;

					if (cur == 10)
						cur = 1;
				}

				return (char)(cur + 48);
			}

			var data = (await File.ReadAllLinesAsync("RealData.txt"))
							.Select(l =>
							{
								var sb = new StringBuilder(l, l.Length * 5);

								for (int a = 1; a < 5; a++)
								{
									foreach (var c in l)
									{
										sb.Append(CalcNext(c, a));
									}
								}

								return sb.ToString();
							})
							.ToList();

			var addLines = data.ToList();

			for (int b = 1; b < 5; b++)
			{
				foreach (var line in data)
				{
					var newLine = string.Concat(line.Select(c => CalcNext(c, b)));
					addLines.Add(newLine);
				}
			}

			data = addLines;

			var graph = new AdjacencyGraph<Point, Edge<Point>>(false);

			for (int row = 0; row < data.Count; row++)
			{
				for (int col = 0; col < data[row].Length; col++)
				{
					graph.AddVertex(new Point(row, col));
				}
			}

			var edgeCosts = new Dictionary<Edge<Point>, int>(graph.EdgeCount);

			int GetWeight(Point p)
			{
				return data[p.X][p.Y] - 48;
			}

			for (int row = 0; row < data.Count; row++)
			{
				for (int col = 0; col < data[row].Length; col++)
				{
					var srcPoint = new Point(row, col);
					var edges = new (Point Point, Func<Point, int> Weight)[]
						{
							(new Point(row - 1, col), GetWeight),
							(new Point(row, col + 1), GetWeight),
							(new Point(row + 1, col), GetWeight),
							(new Point(row, col - 1), GetWeight)
						}
						.Where(t => t.Point.X >= 0 && t.Point.X < data.Count &&
									t.Point.Y >= 0 && t.Point.Y < data[row].Length)
						.Select(t => new Edge<Point>(srcPoint, t.Point))
						.ToList();

					foreach (var edge in edges)
					{
						graph.AddEdge(edge);
						edgeCosts.Add(edge, GetWeight(edge.Target));
					}
				}
			}

			var start = new Point(0, 0);
			var goal = new Point(data.Count - 1, data[0].Length - 1);
			var astar = new AStarShortestPathAlgorithm<Point, Edge<Point>>(graph, e => edgeCosts[e], p => goal.X - p.X + goal.Y - p.Y);

			var pathRecorder = new VertexPredecessorRecorderObserver<Point, Edge<Point>>();
			using (pathRecorder.Attach(astar))
			{
				astar.Compute(start);

				if (pathRecorder.TryGetPath(goal, out var path))
				{
					var cost = path.Select(e => edgeCosts[e])
									.Sum();

					Console.WriteLine("What is the lowest total risk of any path from the top left to the bottom right?");
					Console.WriteLine(cost);
				}
				else
				{
					Console.WriteLine("No path found.");
				}
			}
		}
	}
}
