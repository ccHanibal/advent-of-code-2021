namespace Day16_1
{
	public interface IPaket
	{
		int TypeId { get; }
		long Value { get; }
		long Version { get; }

		long GetVersionSum();
		void Print(string indent);
	}
}
