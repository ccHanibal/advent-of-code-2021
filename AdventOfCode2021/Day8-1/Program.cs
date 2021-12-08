using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day8_1
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var groups = (await File.ReadAllLinesAsync("RealData.txt"))
							.Select(l =>
							{
								var p = l.Split('|');
								return new
								{
									Segemnts = p[0].Split(' '),
									Output = p[1].Split(' ')
								};
							})
							.ToList();

			var uniqueLens = new[] { 2, 4, 3, 7 };
			var numUniqueOutputs = groups.SelectMany(g => g.Output)
										.Where(o => uniqueLens.Contains(o.Length))
										.Count();

			Console.WriteLine("In the output values, how many times do digits 1, 4, 7, or 8 appear?");
			Console.WriteLine(numUniqueOutputs);
		}
	}
}
