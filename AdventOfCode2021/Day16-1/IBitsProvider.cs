using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16_1
{
	public interface IBitsProvider
	{
		string GetBinString();
		IEnumerable<char> GetMessage();
	}
}
