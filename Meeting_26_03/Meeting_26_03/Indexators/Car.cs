using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_26_03.Indexators
{
	class Car
	{
		public string Name { get; set; }
		public string Number { get; set; }
		public override string ToString()
		{
			return $"{Name} - {Number}";
		}
		public class A
		{
			public static int c = 0;
		}
	}
}
