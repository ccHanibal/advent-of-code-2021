using System;
using System.Collections.Generic;

namespace Utils
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

		public static IEnumerable<(T First, T Second, T Third)> Pairwise3<T>(this IEnumerable<T> source)
			where T : struct
		{
			_ = source ?? throw new ArgumentNullException(nameof(source));

			return Pairwise3Impl();

			IEnumerable<(T, T, T)> Pairwise3Impl()
			{
				T? firstItem = null;
				T? secondItem = null;

				foreach (var item in source)
				{
					if (firstItem.HasValue && secondItem.HasValue)
						yield return (firstItem.Value, secondItem.Value, item);

					firstItem = secondItem;
					secondItem = item;
				}
			}
		}
	}
}
