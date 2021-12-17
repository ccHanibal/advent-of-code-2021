using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16_1
{
	public class OpertorPaket : IPaket
	{
		private readonly IList<IPaket> pakets;

		public int TypeId { get; }
		public long Value
		{
			get
			{
				var values = pakets.Select(p => p.Value);

				return TypeId switch
				{
					0 => values.Sum(),
					1 => values.Aggregate(1l, (a, b) => a * b),
					2 => values.Min(),
					3 => values.Max(),
					5 => pakets[0].Value > pakets[1].Value ? 1 : 0,
					6 => pakets[0].Value < pakets[1].Value ? 1 : 0,
					7 => pakets[0].Value == pakets[1].Value ? 1 : 0,
					_ => throw new InvalidOperationException("Unkonwn type ID.")
				};
			}
		}
		public long Version { get; }

		public OpertorPaket(int version, int typeId, IEnumerable<IPaket> pakets)
		{
			this.pakets = pakets.ToList();

			this.TypeId = typeId;
			this.Version = version;
		}

		public long GetVersionSum()
		{
			return pakets.Select(p => p.GetVersionSum())
						.Sum()
					+ Version;
		}

		public void Print(string indent)
		{
			Console.WriteLine(
						"{0}Operator - Version: {1}, Type ID: {2}, Num Pakets: {3}",
						indent, Version, TypeId, pakets.Count);

			foreach (var paket in pakets)
			{
				paket.Print(indent + "  ");
			}
		}
	}
}
