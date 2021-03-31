using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_26_03
{
	public class Base
	{

		public Base()
		{
			LogState();
		}
		public virtual void LogState()
		{
			Console.WriteLine("Base Class with empty state");
		}
	}
	public class Child : Base
	{
		public String Text { get; set; }
		public Child()
		{
			Text = "Bla bla bla";
		}
		public override void LogState()
		{
			Console.WriteLine($"This class contains text with length {Text.Length} characters");
		}
	}
}
