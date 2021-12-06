using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day4_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var parser = new BingoParser("RealData.txt");
			var data = await parser.ParseAsync();

			foreach (var number in data.DrawnNumbers)
			{
				number.Choose();

				var winnerBoard = data.Boards.FirstOrDefault(b => b.IsWin());
				if (winnerBoard is not null)
				{
					Console.WriteLine("What will your final score be if you choose that board?");
					Console.WriteLine(winnerBoard.GetScore() * number.Number);
					return;
				}
			}
		}
	}
}
