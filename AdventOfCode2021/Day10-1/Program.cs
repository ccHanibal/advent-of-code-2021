using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day10_1
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

		public static async Task Main(string[] args)
		{
			var navLines = (await File.ReadAllLinesAsync("RealData.txt"))
										.ToArray();

			var sumInvalidChars = navLines.Select(GetFirstIncorrectChar)
										.Where(c => c is not null)
										.Select(c => c switch
										{
											')' => 3,
											']' => 57,
											'}' => 1197,
											'>' => 25137,
											_ => throw new InvalidOperationException($"Unkown char found >{c}<.")
										})
										.Sum();

			Console.WriteLine("What is the total syntax error score for those errors?");
			Console.WriteLine(sumInvalidChars);
		}

		private static char? GetFirstIncorrectChar(string line)
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
						return c;
				}
			}

			return null;
		}
	}
}
