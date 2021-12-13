using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day12_1;

namespace Day12_2
{
    using System.Linq;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var startCave = await Day12_1.Program.ParseCaves("RealData.txt");

            int numPaths = 0;

            foreach (var cave in startCave.Neighbors)
            {
                var paths = FindPathsToEnd(cave, (new List<Cave> { startCave }, true)).ToList();
                foreach (var path in paths)
                {
                    //Console.WriteLine(string.Join(",", path));

                    // somewhere there was an error here, so check the solution for validity
                    //var set = new HashSet<string>();
                    //bool twiceFound = false;
                    //bool isOk = true;
                    //foreach (var c in path)
                    //{
                    //    if (!set.Add(c.Name) && c.HasLimitedNumberOfVisits)
                    //    {
                    //        if (!twiceFound)
                    //        {
                    //            twiceFound = true;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine(string.Join(",", path));
                    //            Console.WriteLine("[WARN] Fehler gefunden.");
                    //            Console.WriteLine();

                    //            isOk = false;
                    //        }
                    //    }
                    //}

                    //if (isOk)
                       numPaths++;
                }
            }

            Console.WriteLine("How many paths through this cave system are there that visit small caves at most once?");
            Console.WriteLine(numPaths);
        }

        private static IEnumerable<List<Cave>> FindPathsToEnd(Cave cave, (List<Cave> CavesVisited, bool CanVisitOneSmallCaveTwice) currentPath)
        {
            currentPath.CavesVisited.Add(cave);

            if (cave.Name == "end")
            {
                yield return currentPath.CavesVisited;
                yield break;
            }

            var possibileNeighbors = currentPath.CanVisitOneSmallCaveTwice
                        ? cave.Neighbors.ToList()
                        : cave.Neighbors.Where(n => !currentPath.CavesVisited.Where(c => c.HasLimitedNumberOfVisits).Contains(n)).ToList();
            
            if (possibileNeighbors.Any())
            {
                foreach (var neighbor in possibileNeighbors)
                {
                    var hasAnySmallCaveBeenVisitedTwice = !currentPath.CanVisitOneSmallCaveTwice ||
                            currentPath.CavesVisited.Append(neighbor).Where(c => c.HasLimitedNumberOfVisits)
                                    .GroupBy(c => c.Name)
                                    .Select(g => g.Count())
                                    .Any(n => n >= 2);

                    var paths = FindPathsToEnd(neighbor, (currentPath.CavesVisited.ToList(), !hasAnySmallCaveBeenVisitedTwice)).ToList();
                    foreach (var path in paths)
                        yield return path;
                }
            }
        }
    }
}
