using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

namespace Day15_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await File.ReadAllLinesAsync("RealData.txt");

			var graph = new AdjacencyGraph<Point, Edge<Point>>(false);

			for (int row = 0; row < data.Length; row++)
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

			for (int row = 0; row < data.Length; row++)
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
						.Where(t => t.Point.X >= 0 && t.Point.X < data.Length &&
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
			var goal = new Point(data.Length - 1, data[0].Length - 1);
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
