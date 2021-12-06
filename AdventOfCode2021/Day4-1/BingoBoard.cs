using System.Linq;

namespace Day4_1
{
	public class BingoBoard
	{
		private readonly IBingoCell[][] cells;

		public BingoBoard(IBingoCell[][] cells)
		{
			this.cells = cells;
		}

		public int GetScore()
		{
			return cells.SelectMany(c => c)
						.Where(c => !c.IsChosen)
						.Select(c => c.Number)
						.Sum();
		}
		public bool IsWin()
		{
			for (int a = 0; a < 5; a++)
			{
				if (IsWin(a))
					return true;

			}

			return false;

			bool IsWin(int index)
			{
				if (Enumerable.Range(0, 5).All(i => cells[i][index].IsChosen))
					return true;

				if (Enumerable.Range(0, 5).All(i => cells[index][i].IsChosen))
					return true;

				return false;
			}
		}
	}
}
