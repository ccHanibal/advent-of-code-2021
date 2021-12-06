using System;
using System.Linq;
using System.Threading.Tasks;
using Day5_1;

namespace Day5_2
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var parser = new LineParser("RealData.txt");
			var numOverlapingPoints = (await parser.ParseAsync(true))
											.GroupBy(p => p)
											.Where(g => g.Count() > 1)
											.Count();

			Console.WriteLine("At how many points do at least two lines overlap?");
			Console.WriteLine(numOverlapingPoints);
		}
	}
}
