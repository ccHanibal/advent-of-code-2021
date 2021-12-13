using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12_1
{
    public class Cave
    {
        private readonly HashSet<Cave> neighbors = new();

        public bool HasLimitedNumberOfVisits { get; }
        public string Name { get; }
        public IEnumerable<Cave> Neighbors => neighbors;

        public Cave(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            HasLimitedNumberOfVisits = name.All(char.IsLower);
        }

        public void AddNeighbor(Cave cave)
        {
            neighbors.Add(cave);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
