using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3_2
{
	public interface ICommonBitTuple
	{
        char Bit { get; }
		ICollection<string> Items { get; }
    }

	public record CommonBitTuple(char Bit, ICollection<string> Items) : ICommonBitTuple;

    public static class CommonBit
    {
		public static readonly Func<IEnumerable<ICommonBitTuple>, IEnumerable<ICommonBitTuple>> Most =
			s => s.OrderByDescending(t => t.Items.Count)
					.ThenByDescending(t => t.Bit);

		public static readonly Func<IEnumerable<ICommonBitTuple>, IEnumerable<ICommonBitTuple>> Least =
			s => s.OrderBy(t => t.Items.Count)
					.ThenBy(t => t.Bit);
	}

    public static class MostLeastCommonBitExtensions
    {
		public static IEnumerable<string> WithCommonBit(this IEnumerable<string> source, int index, Func<IEnumerable<ICommonBitTuple>, IEnumerable<ICommonBitTuple>> orderSelctor)
		{
			_ = source ?? throw new ArgumentNullException(nameof(source));

			return WithMostCommonBitImpl();

			IEnumerable<string> WithMostCommonBitImpl()
			{
				if (!source.AtLeast(2))
					return source;

				var grouped = source.GroupBy(n => n[index])
									.Select(g => new CommonBitTuple(g.Key, g.ToList()));

				var ordered = orderSelctor(grouped);
				return ordered.Select(t => t.Items)
								.First();
			}
		}
	}
}
