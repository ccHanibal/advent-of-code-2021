using System;
using System.Reflection;

namespace Day_18_1
{
	public record TraverseItem(object Number, int Depth, SnailfishNumber Parent, PropertyInfo PropertyInfo)
	{
		public int GetValue()
		{
			return Number is int i ? i : throw new InvalidOperationException("Number is no integer.");
		}
		public void SetValue(int value)
		{
			PropertyInfo.SetValue(Parent, value);
		}
		public void SetNumber(SnailfishNumber number)
		{
			PropertyInfo.SetValue(Parent, number);
		}
	}
}
