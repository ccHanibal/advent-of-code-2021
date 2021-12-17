using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16_1
{
	public class PaketParser
	{
		private readonly string msg;
		private int currentIndex = -1;

		public PaketParser(string message)
		{
			this.msg = message;
		}

		public IPaket ParsePaket()
		{
			int version = ParseVersion();
			int typeId = ParseTypeId();

			if (typeId == 4)
			{
				var paketValue = new StringBuilder();
				bool hasMoreParts;

				do
				{
					hasMoreParts = GetNextChar() == '1';

					foreach (var c in GetNextCharsCore(4))
						paketValue.Append(c);
				}
				while (hasMoreParts);

				try
				{
					var value = Convert.ToInt64(paketValue.ToString(), 2);

					return new LiteralPaket(version, value);
				}
				catch (Exception ex)
				{
					throw;
				}
			}

			var lengthType = GetNextChar();
			if (lengthType == '0')
			{
				// fixed length
				var fixedLen = ParseInt(15);

				var fixedLengthParser = new PaketParser(msg.Substring(currentIndex + 1, fixedLen));
				var pakets = fixedLengthParser.ParsePakets().ToList();

				currentIndex += fixedLen;

				return new OpertorPaket(version, typeId, pakets);
			}
			else
			{
				// fixed number of pakets
				var numberOfSubPakets = ParseInt(11);

				var pakets = Enumerable.Range(0, numberOfSubPakets)
										.Select(i => ParsePaket())
										.ToList();

				return new OpertorPaket(version, typeId, pakets);
			}
		}

		public IEnumerable<IPaket> ParsePakets()
		{
			var pakets = new List<IPaket>();

			do
			{
				var paket = ParsePaket();
				pakets.Add(paket);
			}
			while (currentIndex < msg.Length - 1);

			return pakets;
		}

		private int ParseVersion()
		{
			return ParseInt(3);
		}

		private int ParseTypeId()
		{
			return ParseInt(3);
		}
		private int ParseInt(int numChars)
		{
			var tmp = GetNextChars(numChars);
			return Convert.ToInt32(tmp, 2);
		}

		private char GetNextChar()
		{
			currentIndex++;

			if (currentIndex < msg.Length)
				return msg[currentIndex];

			throw new InvalidOperationException("End of stream reached.");
		}

		private string GetNextChars(int numChars)
		{
			return string.Concat(GetNextCharsCore(numChars));
		}

		private IEnumerable<char> GetNextCharsCore(int numChars)
		{
			return Enumerable.Range(0, numChars)
							.Select(i => GetNextChar());
		}
	}
}
