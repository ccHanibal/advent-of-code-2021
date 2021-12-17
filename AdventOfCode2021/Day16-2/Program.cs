using System;
using System.IO;
using System.Threading.Tasks;
using Day16_1;

namespace Day16_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await File.ReadAllTextAsync("RealData.txt");
			var parser = new ProtocolParser(
							new HexToBinaryConverter(data));

			var paket = parser.ParseMessage();

			Console.WriteLine("What do you get if you evaluate the expression represented by your hexadecimal-encoded BITS transmission?");
			Console.WriteLine(paket.Value);
		}
	}
}
