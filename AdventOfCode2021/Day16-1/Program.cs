using System;
using System.IO;
using System.Threading.Tasks;

namespace Day16_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var data = await File.ReadAllTextAsync("RealData.txt");
			var parser = new ProtocolParser(
							new HexToBinaryConverter(data));

			var paket = parser.ParseMessage();
			paket.Print("");

			Console.WriteLine();
			Console.WriteLine("What do you get if you add up the version numbers in all packets?");
			Console.WriteLine(paket.GetVersionSum());
		}
	}
}
