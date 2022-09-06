using System;
using System.Collections.Generic;
using System.Reflection;

namespace Day_18_1
{
	public partial class SnailfishNumber
	{
		private static readonly PropertyInfo propInfoLeft = typeof(SnailfishNumber).GetProperty(nameof(Left));
		private static readonly PropertyInfo propInfoRight = typeof(SnailfishNumber).GetProperty(nameof(Right));

		private object left;
		private object right;

		public object Left
		{
			get { return left; }
			set
			{
				if (left is SnailfishNumber sn1)
					sn1.Parent = null;

				this.left = value;

				if (left is SnailfishNumber sn2)
					sn2.Parent = this;
			}
		}
		public object Right
		{
			get { return right; }
			set
			{
				if (right is SnailfishNumber sn1)
					sn1.Parent = null;

				right = value;

				if (right is SnailfishNumber sn2)
					sn2.Parent = this;
			}
		}
		public SnailfishNumber Parent { get; private set; }

		public SnailfishNumber()
		{
		}
		public SnailfishNumber(int left, int right)
		{
			this.Left = left;
			this.Right = right;
		}

		public SnailfishNumber Clone()
		{
			return new SnailfishNumber
			{
				Left = left switch
				{
					int i => i,
					SnailfishNumber sn => sn.Clone(),
					_ => throw new InvalidOperationException()
				},
				Right = right switch
				{
					int i => i,
					SnailfishNumber sn => sn.Clone(),
					_ => throw new InvalidOperationException()
				}
			};
		}

		public int GetMagnitude()
		{
			int leftMgd = GetMagnitude(Left);
			int rightMgd = GetMagnitude(Right);

			return leftMgd * 3 + rightMgd * 2;
		}

		private int GetMagnitude(object part)
		{
			if (part is int i)
				return i;

			if (part is SnailfishNumber sn)
				return sn.GetMagnitude();

			throw new InvalidOperationException($"Unkown part type: {part?.GetType().Name ?? "null"}");
		}

		public override string ToString()
		{
			return $"[{ToString(Left)},{ToString(Right)}]";
		}
		private static string ToString(object numberPart)
		{
			if (numberPart is int i)
				return i.ToString();

			if (numberPart is SnailfishNumber sn)
				return sn.ToString();

			throw new InvalidOperationException($"Unknown type found: {(numberPart?.GetType().FullName ?? "null")}");
		}

		public IEnumerable<TraverseItem> DepthFirstTraverse()
		{
			return DepthFirstTraverseThisNumber(0);
		}

		private IEnumerable<TraverseItem> DepthFirstTraverseThisNumber(int parentDepth)
		{
			foreach (var item in SubTraverse(new TraverseItem(Left, parentDepth + 1, this, propInfoLeft)))
			{
				yield return item;
			}

			foreach (var item in SubTraverse(new TraverseItem(Right, parentDepth + 1, this, propInfoRight)))
			{
				yield return item;
			}

			static IEnumerable<TraverseItem> SubTraverse(TraverseItem item)
			{
				yield return item;

				if (item.Number is SnailfishNumber sn)
				{
					foreach (var subItem in sn.DepthFirstTraverseThisNumber(item.Depth))
					{
						yield return subItem;
					}
				}
			}
		}
	}
}
