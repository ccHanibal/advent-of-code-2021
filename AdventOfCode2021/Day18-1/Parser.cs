using System;
using System.Collections.Generic;
using System.IO;

namespace Day_18_1
{
	public class Parser
	{
		private readonly string file;

		public Parser(string file)
		{
			this.file = file;
		}

		public IEnumerable<SnailfishNumber> Parse()
		{
			foreach (var line in File.ReadAllLines(file))
			{
				yield return ParseLine(line);
			}
		}

		private SnailfishNumber ParseLine(string line)
		{
			int numConsumedChars = ParseSnailfishNumber(line, out var number);
			if (numConsumedChars == line.Length)
				return (SnailfishNumber)number;

			throw new InvalidOperationException($"Parse operation did not consume all chars of the line. ({line.Length} vs {numConsumedChars})");
		}
		private int ParseSnailfishNumber(string tupleStr, out object numberOrPart)
		{
			if (char.IsDigit(tupleStr[0]))
			{
				int nextCommaIdx = tupleStr.IndexOf(',');
				int nextClosingBracketIdx = tupleStr.IndexOf(']');
				int numberOfDigits;

				if (nextCommaIdx >= 0)
				{
					if (nextClosingBracketIdx >= 0)
						numberOfDigits = Math.Min(nextCommaIdx, nextClosingBracketIdx);
					else
						numberOfDigits = nextCommaIdx;
				}
				else
				{
					if (nextClosingBracketIdx >= 0)
						numberOfDigits = nextClosingBracketIdx;
					else
						throw new InvalidOperationException("No comma or closing bracket after digits.");
				}

				var intPart = tupleStr.Substring(0, numberOfDigits);
				try
				{
					int part = int.Parse(intPart);
					numberOrPart = part;

					return numberOfDigits;
				}
				catch (FormatException)
				{
					Console.WriteLine("{0} => {1}", tupleStr, intPart);
					Console.WriteLine("{0}, {1}", 0, numberOfDigits);
					throw;
				}
			}

			if (tupleStr[0] == '[')
			{
				int lengthLeftPart = ParseSnailfishNumber(tupleStr.Substring(1), out var left);
				int lengtRightPart = ParseSnailfishNumber(tupleStr.Substring(1 + lengthLeftPart + 1), out var right);

				numberOrPart = new SnailfishNumber
				{
					Left = left,
					Right = right
				};

				return 1 + lengthLeftPart + 1 + lengtRightPart + 1;
			}

			throw new InvalidOperationException($"Unexpected token found: {tupleStr.ToString()}");
		}
	}
}
