using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day10_2
{
	public static class Program
	{
		private static readonly IDictionary<char, char> ClosingBracketsLookup = new Dictionary<char, char>
		{
			{ '(', ')' },
			{ '[', ']' },
			{ '{', '}' },
			{ '<', '>' }
		};
		private static readonly IDictionary<char, long> ClosingBracketsCompletionScore = new Dictionary<char, long>
		{
			{ ')', 1 },
			{ ']', 2 },
			{ '}', 3 },
			{ '>', 4 }
		};

		public static async Task Main(string[] args)
		{
			var navLines = (await File.ReadAllLinesAsync("RealData.txt"))
										.ToArray();

			var scores = navLines.Select(GetFirstIncorrectCharOrMissingEnd)
									.Where(r => r.Item1 is null)
									.Select(r => CalcCompletionScore(r.Item2))
									.OrderBy(x => x)
									.ToArray();

			Console.WriteLine("What is the middle score?");
			Console.WriteLine(scores[scores.Length / 2]);
		}

		private static (char?, string) GetFirstIncorrectCharOrMissingEnd(string line)
		{
			var closeNeeded = new Stack<char>();

			foreach (var c in line)
			{
				if (ClosingBracketsLookup.TryGetValue(c, out var closingBracket))
				{
					closeNeeded.Push(closingBracket);
				}
				else
				{
					var closingBracketNeeded = closeNeeded.Pop();
					if (closingBracketNeeded != c)
						return (c, null);
				}
			}

			return (null, string.Concat(closeNeeded));
		}

		private static long CalcCompletionScore(string completionString)
		{
			long score = 0;

			foreach (var c in completionString)
			{
				score = (score * 5) + ClosingBracketsCompletionScore[c];
			}

			return score;
		}
	}
}
