using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4_1
{
	public record BingoData(IEnumerable<IChoosableBingoCell> DrawnNumbers, IEnumerable<BingoBoard> Boards);

	public class BingoParser
	{
		private static readonly Regex rowRegex = new Regex(@"^\s*(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s*$");

		private readonly string inputFile;

		public BingoParser(string inputFile)
		{
			this.inputFile = inputFile;
		}

		public async Task<BingoData> ParseAsync()
		{
			var lines = await File.ReadAllLinesAsync(inputFile);

			var drawnNumbers = GetDrawnNumbers();
			var boards = GetBoards().ToList();

			return new BingoData(drawnNumbers.Values, boards);

			Dictionary<int, BingoCell> GetDrawnNumbers()
			{
				int[] nums = lines[0].Split(',')
								.Select(int.Parse)
								.ToArray();

				return nums.ToDictionary(n => n, n => new BingoCell(n));
			}
			IEnumerable<BingoBoard> GetBoards()
			{
				var knownNumbers = new Dictionary<int, BingoCell>(drawnNumbers);
				List<BingoCell[]> currentBoard = null;

				foreach (var line in lines.Skip(2))
				{
					if (string.IsNullOrEmpty(line))
					{
						yield return new BingoBoard(currentBoard.ToArray());
						currentBoard = null;
						continue;
					}

					currentBoard ??= new List<BingoCell[]>(5);

					var matches = rowRegex.Match(line);
					if (!matches.Success)
						throw new InvalidOperationException($"No match at >{line}<.");

					var boardLine = matches.Groups.Values.Skip(1)
											.Select(g => int.Parse(g.Value))
											.Select(n =>
											{
												if (knownNumbers.TryGetValue(n, out var cell))
													return cell;

												cell = new BingoCell(n);
												knownNumbers.Add(n, cell);
												return cell;
											})
											.ToList();

					currentBoard.Add(boardLine.ToArray());
				}

				if (currentBoard is not null)
				{
					if (currentBoard.Count == 5)
						yield return new BingoBoard(currentBoard.ToArray());
					else
						throw new InvalidOperationException($"Last board must have 5 rows, but has {currentBoard.Count}.");
				}
			}
		}
	}
}
