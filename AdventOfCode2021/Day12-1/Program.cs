using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Day12_1
{
    using System.Linq;

    public static class Program
	{
		public static async Task Main(string[] args)
        {
            var startCave = await ParseCaves("RealData.txt");

            int numPaths = 0;

            foreach (var cave in startCave.Neighbors)
            {
                var paths = FindPathsToEnd(cave, new List<Cave>()).ToList();
                foreach (var path in paths)
                {
                    numPaths++;
                }
            }

            Console.WriteLine("How many paths through this cave system are there that visit small caves at most once?");
            Console.WriteLine(numPaths);
        }

        private static IEnumerable<List<Cave>> FindPathsToEnd(Cave cave, List<Cave> currentPath)
        {
            currentPath.Add(cave);

            if (cave.Name == "end")
            {
                yield return currentPath;
                yield break;
            }

            var possibileNeighbors = cave.Neighbors.Where(n => !currentPath.Where(c => c.HasLimitedNumberOfVisits).Contains(n)).ToList();
            if (possibileNeighbors.Any())
            {
                foreach (var neighbor in possibileNeighbors)
                {
                    var paths = FindPathsToEnd(neighbor, currentPath.ToList()).ToList();
                    foreach (var path in paths)
                        yield return path;
                }
            }
        }

        public static async Task<Cave> ParseCaves(string file)
        {
            var lines = await File.ReadAllLinesAsync(file);

            var caves = new Dictionary<string, Cave>(StringComparer.Ordinal);

            foreach (var line in lines)
            {
                var connectedCaves = line.Split('-');

                if (!caves.TryGetValue(connectedCaves[0], out var cave1))
                {
                    cave1 = new Cave(connectedCaves[0]);
                    caves.Add(cave1.Name, cave1);
                }

                if (!caves.TryGetValue(connectedCaves[1], out var cave2))
                {
                    cave2 = new Cave(connectedCaves[1]);
                    caves.Add(cave2.Name, cave2);
                }

                if (cave2.Name != "start")
                    cave1.AddNeighbor(cave2);

                if (cave1.Name != "start")
                cave2.AddNeighbor(cave1);
            }

            return caves["start"];
        }
    }
}
