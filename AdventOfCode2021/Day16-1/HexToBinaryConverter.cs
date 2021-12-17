﻿using System.Collections.Generic;

namespace Day16_1
{
	public class HexToBinaryConverter : IBitsProvider
	{
		private static readonly IDictionary<char, string> BinValueMapper = new Dictionary<char, string>
		{
			{ '0', "0000" },
			{ '1', "0001" },
			{ '2', "0010" },
			{ '3', "0011" },
			{ '4', "0100" },
			{ '5', "0101" },
			{ '6', "0110" },
			{ '7', "0111" },
			{ '8', "1000" },
			{ '9', "1001" },
			{ 'A', "1010" },
			{ 'B', "1011" },
			{ 'C', "1100" },
			{ 'D', "1101" },
			{ 'E', "1110" },
			{ 'F', "1111" },
		};

		private readonly string hexValue;

		public HexToBinaryConverter(string hexValue)
		{
			this.hexValue = hexValue;
		}

		public IEnumerable<char> GetMessage()
		{
			foreach (var c in hexValue)
			{
				foreach (var cc in BinValueMapper[c])
					yield return cc;
			}
		}
		public string GetBinString()
		{
			return string.Concat(GetMessage());
		}
	}
}
