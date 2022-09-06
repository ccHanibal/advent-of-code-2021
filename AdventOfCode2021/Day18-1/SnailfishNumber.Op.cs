using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_18_1
{
	public partial class SnailfishNumber
	{
		public static SnailfishNumber Add(SnailfishNumber n1, SnailfishNumber n2)
		{
			return new SnailfishNumber
			{
				Left = n1.Clone(),
				Right = n2.Clone()
			};
		}
		public static void Reduce(SnailfishNumber number)
		{
			bool success;

			do
			{
				var allParts = number.DepthFirstTraverse().ToList();

				success = Explode(allParts);
				if (!success)
				{
					success = Split(allParts);
				}
			}
			while (success);
		}

		private static bool Explode(List<TraverseItem> allParts)
		{
			var idxPairNested4 = allParts.FindIndex(t => t.Number is SnailfishNumber && t.Depth >= 4);
			if (idxPairNested4 < 0)
				return false;

			var sn = (SnailfishNumber)allParts[idxPairNested4].Number;
			TraverseItem snLeft = FindLeftNeighborNumber(allParts, idxPairNested4);
			TraverseItem snRight = FindRightNeighborNumber(allParts, idxPairNested4);

			snLeft?.SetValue(snLeft.GetValue() + (int)sn.Left);
			snRight?.SetValue(snRight.GetValue() + (int)sn.Right);

			allParts[idxPairNested4].SetValue(0);

			return true;
		}
		private static bool Split(List<TraverseItem> allParts)
		{
			var sn10OrGreater = allParts.FirstOrDefault(t => t.Number is int i && i >= 10);
			if (sn10OrGreater is null)
				return false;

			var value = sn10OrGreater.GetValue();
			sn10OrGreater.SetNumber(new SnailfishNumber
			{
				Left = (int)Math.Floor(value / 2.0),
				Right = (int)Math.Ceiling(value / 2.0)
			});

			return true;
		}

		private static TraverseItem FindLeftNeighborNumber(IList<TraverseItem> allParts, int leftOf)
		{
			for (int a = leftOf - 1; a >= 0; a--)
			{
				if (allParts[a].Number is int)
					return allParts[a];
			}

			return null;
		}
		private static TraverseItem FindRightNeighborNumber(IList<TraverseItem> allParts, int rightOf)
		{
			for (int a = rightOf + 3; a < allParts.Count; a++)
			{
				if (allParts[a].Number is int)
					return allParts[a];
			}

			return null;
		}
	}
}
