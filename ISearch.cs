using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder
{
	interface ISearch
	{
		Task SearchAsync(string text);
	}
}
