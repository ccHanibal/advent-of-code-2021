using System;

namespace Day16_1
{
	public class LiteralPaket : IPaket
	{
		public int TypeId { get; } = 4;
		public long Value { get; }
		public long Version { get; }

		public LiteralPaket(int version, long value)
		{
			this.Value = value;
			this.Version = version;
		}

		public long GetVersionSum()
		{
			return Version;
		}
		public void Print(string indent)
		{
			Console.WriteLine(
						"{0}Literal - Version: {1}, Type ID: {2}, Value: {3}",
						indent, Version, TypeId, Value);
		}
	}
}
