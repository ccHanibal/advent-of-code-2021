using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day5_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var parser = new LineParser("RealData.txt");
			var numOverlapingPoints = (await parser.ParseAsync(false))
											.GroupBy(p => p)
											.Where(g => g.Count() > 1)
											.Count();

			Console.WriteLine("At how many points do at least two lines overlap?");
			Console.WriteLine(numOverlapingPoints);
		}
	}
}
