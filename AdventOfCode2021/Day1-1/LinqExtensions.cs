using System;
using System.Collections.Generic;

namespace Day1_1
{
	public static class LinqExtensions
	{
		public static IEnumerable<(T? Predecessor, T Item)> Pairwise<T>(this IEnumerable<T> source)
			where T : struct
		{
			_ = source ?? throw new ArgumentNullException(nameof(source));

			return PairwiseImpl();

			IEnumerable<(T?, T)> PairwiseImpl()
			{
				T? lastItem = null;

				foreach (var item in source)
				{
					yield return (lastItem, item);
					lastItem = item;
				}
			}
		}
	}
}
