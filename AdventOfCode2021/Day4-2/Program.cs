using System;
using System.Linq;
using System.Threading.Tasks;
using Day4_1;

namespace Day4_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var parser = new BingoParser("RealData.txt");
			var data = await parser.ParseAsync();


			var notWonBoards = data.Boards.ToList();

			foreach (var number in data.DrawnNumbers)
			{
				number.Choose();

				var winnerBoards = notWonBoards.Where(b => b.IsWin()).ToList();
				notWonBoards = notWonBoards.Except(winnerBoards).ToList();
				if (!notWonBoards.Any())
				{
					Console.WriteLine("Once it wins, what would its final score be?");
					Console.WriteLine(winnerBoards.First().GetScore() * number.Number);
					return;
				}
			}
		}
	}
}
