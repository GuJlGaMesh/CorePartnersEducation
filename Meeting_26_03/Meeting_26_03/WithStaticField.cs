using System;

namespace Meeting_26_03
{
	class WithStaticField
	{
		public static string s;

		static WithStaticField()
		{
			s = "initialize me";
			Console.WriteLine("static");
		}
		public WithStaticField()
		{
			Console.WriteLine("usual");
		}
	}
	class WithoutStaticField
	{

		static WithoutStaticField()
		{
			Console.WriteLine("static without field");
		}

	}

}
