namespace Day4_1
{
	public interface IBingoCell
	{
		bool IsChosen { get; }
		int Number { get; }
	}

	public interface IChoosableBingoCell : IBingoCell
	{
		void Choose();
	}

	public class BingoCell : IBingoCell, IChoosableBingoCell
	{
		public bool IsChosen { get; private set; }
		public int Number { get; }

		public BingoCell(int number)
		{
			this.Number = number;
		}

		public void Choose()
		{
			IsChosen = true;
		}
	}
}
